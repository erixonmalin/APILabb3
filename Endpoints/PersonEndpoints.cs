using APILabb3.Data;
using APILabb3.DTO.PersonDTO;
using APILabb3.Models;
using APILabb3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILabb3.Endpoints
{
    public static class PersonEndpoints
    {
        public static void MapPersonEndpoints(this IEndpointRouteBuilder app)
        {
            //GET ALL PERSONS (with repository)
            app.MapGet("*/Get-all-persons", async () => await PersonRepository.GetPersonAsync())
               .WithTags("Person");

            //GET ALL PERSONS WITH HOBBIES (with repository)
            app.MapGet("/get-persons-with-hobbies", async () => await PersonRepository.GetPersonHobbiesAsync())
            .WithTags("Person");


            //CREATE NEW PERSON (with repository)
            app.MapPost(pattern: "/Create-person", handler: async (PersonCreateDto createDTO) =>
            {
                bool createSuccessful = await PersonRepository.CreatePersonAsync(createDTO);
                if (createSuccessful)
                {
                    return Results.Ok(value: "Person was successfully created");
                }
                else
                {
                    return Results.BadRequest();
                }
            }).WithTags("Person");



            //GET PERSON BY ID (with repository)
            app.MapGet(pattern: "/Get-person-by-id/{personId}", handler: async (int personId) =>
            {
                Person personToReturn = await PersonRepository.GetPersonByIdAsync(personId);
                if (personToReturn != null)
                {
                    return Results.Ok(personToReturn);
                }
                else
                {
                    return Results.BadRequest($"No person found with ID: '{personId}'");
                }
            }).WithTags("Person");


            //GET PERSON WITH HOBBIES AND LINK
            app.MapGet("*/Get-person-hobbie-link{firstname}", async (ApplicationDbContext context, [FromQuery] string SearchTerm) =>
            {
                var personHobbiesList = await context.Links
                 .Include(h => h.Hobbies)
                 .Where(h => h.Hobbies.Persons != null)
                 .Where(h => h.FK_HobbiesId != null) 
                 .Select(h => new PersonHobbieLinkGetDto
                 {
                     PersonId = h.Hobbies.Persons.PersonId,
                     FirstName = h.Hobbies.Persons.FirstName,
                     LastName = h.Hobbies.Persons.LastName,
                     HobbieId = h.Hobbies.HobbieId,
                     HobbieTitle = h.Hobbies.Title,
                     HobbieSummary = h.Hobbies.Summary,
                     LinkId = h.LinkId,
                     LinkName = h.LinkName,
                     Url = h.Url,
                 }).ToListAsync();

                if (personHobbiesList.Count == 0)
                {
                    return Results.NotFound("Sorry, Something went wrong, please try again!");
                }

                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    personHobbiesList = personHobbiesList.Where(p => p.FirstName.StartsWith(SearchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (personHobbiesList.Count == 0)
                    {
                        return Results.Ok($"Sorry, nothing was found with the given term '{SearchTerm}'.");
                    }
                }

                return Results.Ok(personHobbiesList);
            }).WithTags("Person");


            //UPDATE LINK AND HOBBIE FOR PERSON
            app.MapPut("*/Update-link-hobbies-for-person/{personId}", async (ApplicationDbContext context, PersonLinkHobbieUpdateDto updateDto, int id) =>
            {
                var updatePersonLinkHobbie = await context.Links.FindAsync(id);

                if (updatePersonLinkHobbie is null)
                    return Results.NotFound($"Sorry, person with id: '{id}' doesn´t exist.");

                updatePersonLinkHobbie.FK_HobbiesId = updateDto.HobbieId;
                updatePersonLinkHobbie.LinkId = updateDto.LinkId;
                updatePersonLinkHobbie.LinkName = updateDto.LinkName;
                updatePersonLinkHobbie.Url = updateDto.Url;
                await context.SaveChangesAsync();

                return Results.Ok(await context.Links.ToListAsync());
            }).WithTags("Link");


            //DELETE PERSON WITH ID
            app.MapDelete("/Delete-person/{personId}", async (ApplicationDbContext context, int id) =>
            {
                var person = await context.Persons.FindAsync(id);

                if (person is null)
                    return Results.NotFound($"Sorry, there is no person with the id: '{id}'");

                context.Persons.Remove(person);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Persons.ToListAsync());
            }).WithTags("Person");
        }
    }
}

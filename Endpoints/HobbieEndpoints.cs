using APILabb3.Data;
using APILabb3.DTO.HobbieDTO;
using APILabb3.DTO.PersonDTO;
using APILabb3.Models;
using APILabb3.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APILabb3.Endpoints
{
    static public class HobbieEndpoints
    {
        public static void MapHobbieEndpoints(this IEndpointRouteBuilder app)
        {
            //GET ALL HOBBIES (with repository)
            app.MapGet("/Get-all-Hobbies", async () => await HobbiesRepository.GetHobbiesAsync())
               .WithTags("Hobbie");


            //GET HOBBIE BY FIRSTNAME
            app.MapGet("*/Get-person-hobbie-by-firstname", async (ApplicationDbContext context, [FromQuery] string SearchTerm) =>
            {
                var personHobbie = await context.Hobbies
                 .Include(p => p.Persons)
                 .Where(p => p.Persons != null) 
                 .Select(p => new PersonGetDto
                 {
                     PersonId = p.Persons.PersonId,
                     FirstName = p.Persons.FirstName,
                     LastName = p.Persons.LastName,
                     HobbieId = p.HobbieId,
                     Title = p.Title,
                     Summary = p.Summary,

                 }).ToListAsync();

                if (personHobbie.Count == 0)
                {
                    return Results.NotFound("Sorry, no hobbies found in database.");
                }

                if (!string.IsNullOrEmpty(SearchTerm))
                {
                    personHobbie = personHobbie.Where(p => p.FirstName.StartsWith(SearchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (personHobbie.Count == 0)
                    {
                        return Results.Ok($"Sorry, nothing was found with the given term '{SearchTerm}'.");
                    }
                }

                return Results.Ok(personHobbie);
            }).WithTags("Hobbie");


            //CREATE NEW HOBBY TO PERSON
            app.MapPost("*/Create-hobbie-to-person", async (ApplicationDbContext context, PersonHobbieCreateDto createDto) =>
            {
                var personHobbie = new Hobbie
                {
                    Title = createDto.HobbieTitle,
                    Summary = createDto.Summary,
                    FK_PersonId = createDto.PersonId,
                };

                context.Hobbies.Add(personHobbie);
                await context.SaveChangesAsync();

                if (await context.Hobbies.AnyAsync())
                {
                    return Results.Ok(await context.Hobbies.ToListAsync());
                }
                else
                {
                    return Results.NotFound("Sorry, Something went wrong, please try again!");
                }
            }).WithTags("Hobbie");


            //UPDATE HOBBY TO PERSON
            app.MapPut("*/Update-hobby-on-person/{hobbieid}", async (ApplicationDbContext context, PersonHobbieUpdateDto updateDTO, int id) =>
            {
                var updateHobbiePerson = await context.Hobbies.FindAsync(id);

                if (updateHobbiePerson is null)
                    return Results.NotFound("Sorry, this person doesn´t exist.");

                updateHobbiePerson.Title = updateDTO.HobbieTitle;
                updateHobbiePerson.Summary = updateDTO.HobbieSummary;
                updateHobbiePerson.FK_PersonId = updateDTO.PersonId;
                await context.SaveChangesAsync();

                return Results.Ok(await context.Hobbies.ToListAsync());
            }).WithTags("Hobbie");


            //DELETE HOBBIE WITH ID
            app.MapDelete("/Delete-hobbie/{hobbieId}", async (ApplicationDbContext context, int id) =>
            {
                var hobbie = await context.Hobbies.FindAsync(id);

                if (hobbie is null)
                    return Results.NotFound($"No hobbie found with ID: {id}");

                context.Hobbies.Remove(hobbie);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Hobbies.ToListAsync());
            }).WithTags("Hobbie");
        }
    }
}

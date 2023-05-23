using APILabb3.Data;
using APILabb3.DTO.LinkDTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APILabb3.Models;

namespace APILabb3.Endpoints
{
    public static class LinkEndpoints
    {
        public static void MapLinkEndpoints(this IEndpointRouteBuilder app)
        {
            //GET LINKS
            app.MapGet("/Get-links-with/without-linkname/{linkname}", async (ApplicationDbContext context, [FromQuery] string searchTerm) =>
            {
                var link = await context.Links
                 .Select(h => new LinkGetDto
                 {
                     LinkId = h.LinkId,
                     LinkName = h.LinkName,
                     Url = h.Url,

                 }).ToListAsync();

                if (link.Count == 0)
                {
                    return Results.NotFound("Sorry, Something went wrong, please try again!");
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    link = link.Where(l => l.LinkName.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (link.Count == 0)
                    {
                        return Results.Ok($"Sorry, nothing was found with the given term '{searchTerm}'.");
                    }
                }

                return Results.Ok(link);
            }).WithTags("Link");


            //POST NEW PERSON LINK
            app.MapPost("*/Create-link-to-hobbie", async (ApplicationDbContext context, LinkHobbieCreateDto createDto) =>
            {
                var linkHobbie = new Link
                {
                    LinkId = createDto.LinkId,
                    LinkName = createDto.LinkName,
                    Url = createDto.Url,
                    FK_HobbiesId = createDto.HobbiesId,
                };

                context.Links.Add(linkHobbie);
                await context.SaveChangesAsync();

                if (await context.Hobbies.AnyAsync())
                {
                    return Results.Ok(await context.Links.ToListAsync());
                }
                else
                {
                    return Results.NotFound("Sorry, no hobby was added to the database.");
                }
            }).WithTags("Link");


            //GET PERSONS WITH LINKS
            app.MapGet("/Get-persons-with-links/{firstname}", async (ApplicationDbContext context, [FromQuery] string searchTerm) =>
            {
                var personLink = await context.Links
                    .Include(h => h.Hobbies)
                    .Where(h => h.Hobbies.Persons != null)
                    .Where(h => h.Hobbies != null)
                    .Select(h => new PersonGetLinkDto
                    {
                        HobbieId = h.Hobbies.HobbieId,
                        PersonId = h.Hobbies.Persons.PersonId,
                        FirstName = h.Hobbies.Persons.FirstName,
                        LastName = h.Hobbies.Persons.LastName,
                        LinkId = h.LinkId,
                        LinkName = h.LinkName,
                        Url = h.Url,
                    })
                    .ToListAsync();

                if (personLink.Count == 0)
                {
                    return Results.NotFound("Sorry, Something went wrong, please try again!");
                }

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    personLink = personLink.Where(p => p.FirstName.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase))
                        .ToList();

                    if (personLink.Count == 0)
                    {
                        return Results.Ok($"Sorry, nothing was found with the given term '{searchTerm}'.");
                    }
                }

                return Results.Ok(personLink);
            }).WithTags("Link");


           
            //UPDATE LINK
            app.MapPut("/Update-link-for-person-hobbies/{linkId}", async (ApplicationDbContext context, LinkPersonHobbieUpdateDto updateDTO, int id) =>
            {
                var updateLinkPersonHobbie = await context.Links.FindAsync(id);

                if (updateLinkPersonHobbie is null)
                    return Results.NotFound($"Sorry, link with id: '{id}' doesn´t exist.");

                updateLinkPersonHobbie.LinkName = updateDTO.LinkName;
                updateLinkPersonHobbie.Url = updateDTO.Url;
                await context.SaveChangesAsync();

                return Results.Ok(await context.Links.ToListAsync()); 
            }).WithTags("Link");


            //DELETE LINK
            app.MapDelete("/Delete-link/{linkId}", async (ApplicationDbContext context, int id) =>
            {
                var link = await context.Links.FindAsync(id);

                if (link is null)
                    return Results.NotFound($"Sorry, link with id: '{id}' doesn´t exist.");

                context.Links.Remove(link);
                await context.SaveChangesAsync();
                return Results.Ok(await context.Links.ToListAsync());
            }).WithTags("Link");
        }
    }
}

using Microsoft.EntityFrameworkCore;
using APILabb3.Data;
using APILabb3.DTO.PersonDTO;

using APILabb3.Models;

namespace APILabb3.Repositories
{
    internal static class PersonRepository
    {
        //Hämtar alla personer från databasen
        internal async static Task<List<Person>> GetPersonAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Persons.ToListAsync();
            }
        }


        //Hämta alla personer med deras intressen från databasen
        internal async static Task<List<PersonGetDto>> GetPersonHobbiesAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                var personHobbie = await db.Hobbies
                    .Include(i => i.Persons)
                    .Where(i => i.Persons != null) 
                    .Select(i => new PersonGetDto
                    {
                        PersonId = i.Persons.PersonId,
                        FirstName = i.Persons.FirstName,
                        LastName = i.Persons.LastName,
                        HobbieId = i.HobbieId,
                        Title = i.Title,
                        Summary = i.Summary
                    }).ToListAsync();

                return personHobbie;
            }
        }


        //Lägg till ny person
        internal static async Task<bool> CreatePersonAsync(PersonCreateDto createDto)
        {
            using (var db = new ApplicationDbContext())
            {
                var person = new Person
                {
                    FirstName = createDto.FirstName,
                    LastName = createDto.LastName,
                    PhoneNumber = createDto.PhoneNumber,
                };

                db.Persons.Add(person);
                await db.SaveChangesAsync();


                if (await db.Persons.AnyAsync())
                {
                    Results.Ok(createDto);
                }
                else
                {
                    Results.NotFound("Sorry, Something went wrong, please try again!");
                }
                return true;
            }
        }


        //Hämta person efter id
        internal async static Task<Person> GetPersonByIdAsync(int personId)
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Persons.FirstOrDefaultAsync(p => p.PersonId == personId);
            }
        }
    }
}

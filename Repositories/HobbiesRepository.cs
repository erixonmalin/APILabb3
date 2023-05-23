using APILabb3.Data;
using APILabb3.Models;
using Microsoft.EntityFrameworkCore;

namespace APILabb3.Repositories
{
    internal static class HobbiesRepository
    {
        //Hämtar alla hobbies från databasen
        internal async static Task<List<Hobbie>> GetHobbiesAsync()
        {
            using (var db = new ApplicationDbContext())
            {
                return await db.Hobbies.ToListAsync();
            }
        }
    }
}

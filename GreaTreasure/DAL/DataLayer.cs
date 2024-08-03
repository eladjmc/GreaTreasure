using Microsoft.EntityFrameworkCore;
using GreaTreasure.Models;

namespace GreaTreasure.DAL
{
    public class DataLayer : DbContext
    {
        
        public DataLayer(string connectionString) : base(GetOptions(connectionString))
        {
            Database.EnsureCreated();
            Seed();
        }

        private void Seed()
        {
           
            if (Libraries.Any()) return;

            Library library = new Library
            {
                Genre = "ז'אנר כללי",
                Name = "ספרייה מרכזית",
                Width = 1000
            };
            Libraries.Add(library);
            SaveChanges();
            
        }

        private static DbContextOptions GetOptions(string connectionString)
        {
            //SqlServerDbContextOptionsExtensions
            return new DbContextOptionsBuilder()
                .UseSqlServer(connectionString)
                .Options;
        }

        public DbSet<Library> Libraries { get; set; }
        public DbSet<Shelf> Shelves { get; set; }
        public DbSet<Book> Books { get; set; }
    }
}
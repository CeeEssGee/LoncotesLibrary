using LoncotesLibrary.Models;
using Microsoft.EntityFrameworkCore;

public class LoncotesLibraryDbContext : DbContext

{
    public DbSet<MaterialType> MaterialTypes { get; set; }
    public DbSet<Patron> Patrons { get; set; }
    public DbSet<Genre> Genres { get; set; }
    public DbSet<Material> Materials { get; set; }
    public DbSet<Checkout> Checkouts { get; set; }


    // add database
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // seed data for material types
        modelBuilder.Entity<MaterialType>().HasData(new MaterialType[]
        {
            new MaterialType {Id = 1, Name = "Book", CheckoutDays = 14},
            new MaterialType {Id = 2, Name = "Periodical", CheckoutDays = 7},
            new MaterialType {Id = 3, Name = "CD", CheckoutDays = 10}
        });

        // seed data with patrons
        modelBuilder.Entity<Patron>().HasData(new Patron[]
        {
            new Patron {Id = 1, FirstName = "Suzy", LastName = "Jones", Address = "123 Fake Street", Email = "Suzy@gmail.comx", IsActive = true},
            new Patron {Id = 2, FirstName = "Bob", LastName = "Jones", Address = "456 Main Street", Email = "Bob@gmail.comx", IsActive = true}
        });

        // seed data with genres
        modelBuilder.Entity<Genre>().HasData(new Genre[]
        {
            new Genre {Id = 1, Name = "Fantasy"},
            new Genre {Id = 2, Name = "History"},
            new Genre {Id = 3, Name = "Romance"},
            new Genre {Id = 4, Name = "Horror"},
            new Genre {Id = 5, Name = "Nonfiction"},
            new Genre {Id = 6, Name = "Music"}
        });

        // seed data with materials
        modelBuilder.Entity<Material>().HasData(new Material[]
        {
            new Material {Id = 1, MaterialName = "Romeo and Juliet", MaterialTypeId = 1, GenreId = 3},
            new Material {Id = 2, MaterialName = "Shakespeare Biography", MaterialTypeId = 1, GenreId = 5},
            new Material {Id = 3, MaterialName = "Best of the 90s", MaterialTypeId = 3, GenreId = 6},
            new Material {Id = 4, MaterialName = "The Notebook", MaterialTypeId = 1, GenreId = 3},
            new Material {Id = 5, MaterialName = "How to Win Friends and Influence People", MaterialTypeId = 3, GenreId = 5},
            new Material {Id = 6, MaterialName = "It", MaterialTypeId = 1, GenreId = 4},
            new Material {Id = 7, MaterialName = "The Eyes of the Dragon", MaterialTypeId = 1, GenreId = 1},
            new Material {Id = 8, MaterialName = "The Wheel of Time", MaterialTypeId = 1, GenreId = 1},
            new Material {Id = 9, MaterialName = "Time Magazine", MaterialTypeId = 2, GenreId = 5},
            new Material {Id = 10, MaterialName = "World History", MaterialTypeId = 1, GenreId = 2}
        });
    }


    public LoncotesLibraryDbContext(DbContextOptions<LoncotesLibraryDbContext> context) : base(context)
    {

    }
}
using LoncotesLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http.Json;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// allows passing datetimes without time zone data 
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

// allows our api endpoints to access the database through Entity Framework Core
builder.Services.AddNpgsql<LoncotesLibraryDbContext>(builder.Configuration["LoncotesLibraryDbConnectionString"]);

// Set the JSON serializer options
builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// get all materials - The librarians would like to see a list of all the circulating materials. Include the Genre and MaterialType. Exclude materials that have a OutOfCirculationSince value.
// app.MapGet("/api/materials", (LoncotesLibraryDbContext db, int? materialTypeId, int? genreId) =>
// {
//     var foundMaterials = db.Materials
//     .Where(m => m.OutOfCirculationSince == null);

//     return db.Materials.ToList();
// });

// The librarians would like to see a list of all the circulating materials. Include the Genre and MaterialType. Exclude materials that have a OutOfCirculationSince value.
// The librarians also like to search for materials by genre and type. Add query string parameters to the above endpoint for materialTypeId and genreId. Update the logic of the endpoint to include both, either, or neither of these filters, depending which are passed in. Remember, query string parameters are always optional when making an HTTP request, so you have to account for the possibility that any of them will be missing.
app.MapGet("/api/materials", (LoncotesLibraryDbContext db, int? materialTypeId, int? genreId) =>
{
    var query = db.Materials
    .Include(m => m.Genre)
    .Include(m => m.MaterialType)
    .Where(m => m.OutOfCirculationSince == null);
    // .Select(m => new Material
    // {
    //     Id = m.Id,
    //     MaterialName = m.MaterialName,
    //     MaterialTypeId = m.MaterialTypeId,
    //     GenreId = m.GenreId,
    //     OutOfCirculationSince = m.OutOfCirculationSince,
    //     // MaterialType = db.MaterialTypes.FirstOrDefault(mt => m.Id == m.MaterialTypeId),
    //     // Genre = db.Genres.FirstOrDefault(g => g.Id == m.GenreId)
    // })
    // .ToList();

    if (materialTypeId.HasValue)
    {
        query = query
        .Where(m => m.MaterialTypeId == materialTypeId);
    }

    if (genreId.HasValue)
    {
        query = query
        .Where(g => g.GenreId == genreId);
    }


    return query.OrderBy(m => m.Id).ToList();
});

// The librarians would like to see details for a material. Include the Genre, MaterialType, and Checkouts (as well as the Patron associated with each checkout using ThenInclude).
app.MapGet("/api/materials/{materialId}", (LoncotesLibraryDbContext db, int materialId) =>
{
    return Results.Ok(db.Materials
    .Include(m => m.Genre) // material to m.Genre
    .Include(m => m.MaterialType) // material to m.MaterialType
    .Include(m => m.Checkouts) // material to m.Checkouts (added)
    .ThenInclude(c => c.Patron) // checkout to c.Patron
    .SingleOrDefault(m => m.Id == materialId)
    );

});

// Materials are often added to the library's collection. Add an endpoint to create a new material
app.MapPost("/api/materials", (LoncotesLibraryDbContext db, Material material) =>
{
    db.Materials.Add(material);
    db.SaveChanges();
    return Results.Created($"/api/materials/{material.Id}", material);
});

// Add an endpoint that expects an id in the url, which sets the OutOfCirculationSince property of the material that matches the material id to DateTime.Now. (This is called a soft delete, where a row is not deleted from the database, but instead has a flag that says the row is no longer active.) The endpoint to get all materials should already be filtering these items out.
app.MapPut("/api/materials/outofcirc/{materialId}", (LoncotesLibraryDbContext db, int materialId) =>
{
    Material foundMaterial = db.Materials.SingleOrDefault(m => m.Id == materialId);
    if (foundMaterial == null)
    {
        return Results.NotFound();
    }
    foundMaterial.OutOfCirculationSince = DateTime.Now;
    db.SaveChanges();
    return Results.NoContent();
});

// The librarians will need a form in their app that let's them choose material types. Create an endpoint that retrieves all of the material types to eventually populate that form field
app.MapGet("/api/materialtypes", (LoncotesLibraryDbContext db) =>
{
    return db.MaterialTypes.ToList();
});

// The librarians will also need form fields that have all of the genres to choose from. Create an endpoint that gets all of the genres.
app.MapGet("/api/genres", (LoncotesLibraryDbContext db) =>
{
    return db.Genres.ToList();
});

// The librarians want to see a list of library patrons.
app.MapGet("/api/patrons", (LoncotesLibraryDbContext db) =>
{
    return db.Patrons
    .Include(p => p.Checkouts)
    .ThenInclude(c => c.Material)
    .ThenInclude(m => m.MaterialType)
    .ToList();
});

// This endpoint should get a patron and include their checkouts, and further include the materials and their material types.
app.MapGet("/api/patrons/{patronId}", (LoncotesLibraryDbContext db, int patronId) =>
{
    var query = db.Patrons
    .Include(c => c.Checkouts) // patron to Checkouts
    .ThenInclude(co => co.Material) // c/o to Material
    .ThenInclude(m => m.MaterialType) // m to MaterialType
    .SingleOrDefault(p => p.Id == patronId);
    return Results.Ok(query);
});


// Sometimes patrons move or change their email address. Add an endpoint that updates these properties only.
app.MapPut("/api/patrons/{patron.Id}", (LoncotesLibraryDbContext db, Patron patron) =>
{
    Patron patronToUpdate = db.Patrons.SingleOrDefault(p => p.Id == patron.Id);
    if (patronToUpdate == null)
    {
        return Results.NotFound();
    }
    patronToUpdate.Address = patron.Address;
    patronToUpdate.Email = patron.Email;

    db.SaveChanges();
    return Results.NoContent();
});

// Sometimes patrons move out of the county. Allow the librarians to deactivate a patron (another soft delete example!).
app.MapPut("/api/patrons/deactivate/{patronId}", (LoncotesLibraryDbContext db, int patronId) =>
{
    Patron patronToUpdate = db.Patrons.SingleOrDefault(patron => patron.Id == patronId);
    if (patronToUpdate == null)
    {
        return Results.NotFound();
    }
    patronToUpdate.IsActive = false;

    db.SaveChanges();
    return Results.NoContent();
});

// Activate a person
app.MapPut("/api/patrons/activate/{patronId}", (LoncotesLibraryDbContext db, int patronId) =>
{
    Patron patronToUpdate = db.Patrons.SingleOrDefault(patron => patron.Id == patronId);
    if (patronToUpdate == null)
    {
        return Results.NotFound();
    }
    patronToUpdate.IsActive = true;

    db.SaveChanges();
    return Results.NoContent();
});

// The librarians need to be able to checkout items for patrons. Add an endpoint to create a new Checkout for a material and patron. Automatically set the checkout date to DateTime.Today.
app.MapPost("/api/checkouts", (LoncotesLibraryDbContext db, Checkout newCheckout) =>
{
    newCheckout.CheckoutDate = DateTime.Today;
    // newCheckout.Material = db.Materials.SingleOrDefault(m => m.Id == newCheckout.MaterialId);
    // newCheckout.Patron = db.Patrons.SingleOrDefault(p => p.Id == newCheckout.PatronId);
    db.Checkouts.Add(newCheckout);
    db.SaveChanges();

    Checkout newerCheckout = db.Checkouts
    .Include(c => c.Material)
    .ThenInclude(m => m.MaterialType)
    .SingleOrDefault(c => c.Id == newCheckout.Id);
    return Results.Created($"/api/checkouts/{newCheckout.Id}", newerCheckout);
});

// The librarians need an endpoint to mark a checked out item as returned by item id. Add an endpoint expecting a checkout id, and update the checkout with a return date of DateTime.Today.
app.MapPut("/api/checkouts/return/{id}", (LoncotesLibraryDbContext db, int id) =>
{
    Checkout checkoutToReturn = db.Checkouts.SingleOrDefault(checkout => checkout.Id == id);
    if (checkoutToReturn == null)
    {
        return Results.NotFound();
    }
    checkoutToReturn.ReturnDate = DateTime.Today;
    db.SaveChanges();
    return Results.NoContent();
});

// Let's create an endpoint to get all materials (with their genre and material type) that are currently available (not checked out, and not removed from circulation). A checked out material will have a related checkout that has a ReturnDate of null.
app.MapGet("/api/materials/available", (LoncotesLibraryDbContext db) =>
{
    return db.Materials
    .Include(m => m.Genre) // material to m.Genre
    .Include(m => m.MaterialType) // material to m.MaterialType
    .Where(m => m.OutOfCirculationSince == null)
    .Where(m => m.Checkouts.All(co => co.ReturnDate != null))
    .OrderBy(m => m.Id)
    .ToList();
});

// The librarians want to see all overdue checkouts so that they can send emails to the patrons that need to return the books. Let's create an endpoint that does this.
app.MapGet("/api/checkouts/overdue", (LoncotesLibraryDbContext db) =>
{
    return db.Checkouts
    .Include(p => p.Patron)
    .Include(co => co.Material)
    .ThenInclude(m => m.MaterialType)
    .Where(co =>
        (DateTime.Today - co.CheckoutDate).Days >
        co.Material.MaterialType.CheckoutDays &&
        co.ReturnDate == null)
    .ToList();
});

app.MapGet("/api/checkouts", (LoncotesLibraryDbContext db) =>
{
    return db.Checkouts
    .Include(p => p.Patron)
    .Include(co => co.Material)
    .ThenInclude(m => m.MaterialType)
    .ToList();
});

app.Run();
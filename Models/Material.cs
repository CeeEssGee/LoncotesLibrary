using System.ComponentModel.DataAnnotations;

namespace LoncotesLibrary.Models;

public class Material
{
    public int Id { get; set; }
    [Required]
    public string MaterialName { get; set; }
    public int MaterialTypeId { get; set; }
    public int GenreId { get; set; }
    public DateTime? OutOfCirculationSince { get; set; }
    public MaterialType MaterialType { get; set; }
}
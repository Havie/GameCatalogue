using System.ComponentModel.DataAnnotations; //for [Required] attribute 

namespace GameCatalogue.Client.Models; //file scoped namespace! C#10 feature



public class Game
{
  public int Id { get; set; } = -1;
  [Required] [StringLength(50)]
  public required string GameName { get; set; } = string.Empty; //C#11 feature "required"
  [Required] [StringLength(25)]
  public required string Genre { get; set; } = string.Empty;
  [Required] [Range(0, 100)]
  public decimal Price { get; set; }
  public required DateTime ReleaseDate { get; set; }
}
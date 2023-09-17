
namespace GameCatalogue.Client.Models; //file scoped namespace! C#10 feature

public class Game
{
  public int Id { get; set; } = -1;
  public required string GameName { get; set; } = string.Empty; //C#11 feature "required"
  public required string Genre { get; set; } = string.Empty;
  public decimal Price { get; set; }
  public required DateTime ReleaseDate { get; set; }
}
using GameCatalogue.Client.Models;

namespace GameCatalogue.Client;

public static class GameClient
{

  public static Game[] GetGames() => _games.ToArray();  
  public static IReadOnlyList<Game> Games => _games;
  private static readonly List<Game> _games = new() 
  {
        //Static example data
    
        new Game() { Id = 1, GameName = "Super Mario", Genre = "Platformer", Price = 59.99m, ReleaseDate = new DateTime(1985,9,13)},
        new Game() { Id = 1, GameName = "Street Fighter", Genre = "Fighting", Price = 59.99m, ReleaseDate = new DateTime(1991,2,1)},
        new Game() { Id = 1, GameName = "Notoris: The Goblin War", Genre = "Strategy", Price = 4.99m, ReleaseDate = new DateTime(2021,7, 1)},
  };


}
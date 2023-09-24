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
        new Game() { Id = 2, GameName = "Street Fighter", Genre = "Fighting", Price = 59.99m, ReleaseDate = new DateTime(1991,2,1)},
        new Game() { Id = 3, GameName = "Notoris: The Goblin War", Genre = "Strategy", Price = 4.99m, ReleaseDate = new DateTime(2021,7, 1)},
  };

  public static Game GetGame(int gameID)
  {
    return _games.Find(game => game.Id == gameID) ?? throw new Exception($"Game with id #{gameID} not found");
  }

  public static void AddGame(Game newGame) 
  {
      //newGame.Id = _games.Count;
      newGame.Id = Games.Max(x => x.Id) + 1; //instead of using count, find the largest ID and add one to it.
     _games.Add(newGame);
  }

  public static void UpdateGame(Game updatedGame)
  {
    var existingGame = GetGame(updatedGame.Id); //throws an exception if not found
    //Update the existing games fields with the new values
    existingGame.GameName = updatedGame.GameName;
    existingGame.Genre = updatedGame.Genre;
    existingGame.Price = updatedGame.Price;
    existingGame.ReleaseDate = updatedGame.ReleaseDate;
  }

  public static void DeleteGame(int gameID)
 {
    var existingGame = GetGame(gameID); //throws an exception if not found
    _games.Remove(existingGame);  
 }

}
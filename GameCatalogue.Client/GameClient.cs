using GameCatalogue.Client.Models;
using System.Net.Http.Json; //methods to retrieve/send json data thru API


namespace GameCatalogue.Client;

public  class GameClient
{
  private readonly HttpClient _httpClient;
   /*******************************************************************************/
  public GameClient(HttpClient httpClient)
  {
    _httpClient = httpClient;
  }
  /*******************************************************************************/
  public async Task<Game[]?> GetGamesAsync() 
  {
    //string urlRoute = "/games" without the / in GameServer.Program 
    return await _httpClient.GetFromJsonAsync<Game[]>("games");
  } 

  public async Task<Game?> GetGameAsync(int gameID)
  {
    return await _httpClient.GetFromJsonAsync<Game>($"games/{gameID}") ?? throw new Exception("Failed to find game");
  }

  public async Task AddGameAsync(Game newGame) 
  {
     await _httpClient.PostAsJsonAsync<Game>("games", newGame);
  }

  public async Task UpdateGameAsync(Game updatedGame)
  {
    await _httpClient.PutAsJsonAsync<Game>($"games/{updatedGame.Id}", updatedGame);
  }

  public async Task DeleteGameAsync(int gameID)
 {
    await _httpClient.DeleteFromJsonAsync<Game>($"games/{gameID}");
 }

  /*******************************************************************************/

}
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
  public async Task<Game[]?> GetGamesAsync(string? filter) 
  {
    //string urlRoute = "/games" without the / in GameServer.Program
    //"?filter={filter}" is a delimiter that separates the base URL from the query string in a URL.
    //The portion of the URL before the question mark represents the base URL, which typically identifies the resource or endpoint you want to access
    //When a GET request is made to the root URL with a query string like "/?filter=somevalue", the ASP.NET Core routing system automatically binds the value to the passed in filter parameter.
    return await _httpClient.GetFromJsonAsync<Game[]>($"games?filter={filter}");
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
using GameCatalogue.Server.Models;

//Server side games 
List<Game> _games = new() 
  {
        //example data
        new Game() { Id = 1, GameName = "Super Mario", Genre = "Platformer", Price = 59.99m, ReleaseDate = new DateTime(1985,9,13)},
        new Game() { Id = 2, GameName = "Street Fighter", Genre = "Fighting", Price = 59.99m, ReleaseDate = new DateTime(1991,2,1)},
        new Game() { Id = 3, GameName = "Notoris: The Goblin War", Genre = "Strategy", Price = 4.99m, ReleaseDate = new DateTime(2021,7, 1)},
  };



var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();
var routeGroup = app.MapGroup("/games");

//GET /games
routeGroup.MapGet("/", () => _games);

//GET /games/{id}
routeGroup.MapGet("/{id}", (int id) =>
{
   Game? game = _games.Find(game => game.Id == id);
   if(game is null)
   {
      return Results.NotFound();
   }
   return Results.Ok(game);
});


app.Run();

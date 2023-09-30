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
string urlRoute = "/games";
string getNameAPIFuncRouteEndpointStr = "GetGame";
var routeGroup = app.MapGroup(urlRoute);

//POST /games   (create)
routeGroup.MapPost("/", (Game game) =>
{
   game.Id = _games.Max(x => x.Id) + 1; //instead of using count, find the largest ID and add one to it.
   _games.Add(game);
   //return the name of route that can be used to get the new game aka --> GET /games/{id}
   return Results.CreatedAtRoute(getNameAPIFuncRouteEndpointStr, new {id = game.Id} , game);
});

//GET /games (read)
routeGroup.MapGet("/", () => _games);

//GET /games/{id} (read)
routeGroup.MapGet("/{id}", (int id) =>
{
   Game? game = _games.Find(game => game.Id == id);
   if(game is null)
   {
      return Results.NotFound();
   }
   return Results.Ok(game);
})
.WithName(getNameAPIFuncRouteEndpointStr);


//PUT /games/{id} (update)
routeGroup.MapPut("/{id}", (int id, Game updatedGame) =>
{
    Game? game = _games.Find(game => game.Id == id);
    if(game is null)
    {
        //we could create one with the new id, or return a 404 instead
        //return Results.NotFound();
        updatedGame.Id  = id;
        _games.Add(updatedGame);
        return Results.CreatedAtRoute(getNameAPIFuncRouteEndpointStr, new {id = updatedGame.Id} , updatedGame);
    }
    //Update the existing games fields with the new values
    game.GameName = updatedGame.GameName;
    game.Genre = updatedGame.Genre;
    game.Price = updatedGame.Price;
    game.ReleaseDate = updatedGame.ReleaseDate;
    return Results.NoContent(); //could also return Results.Ok(game) but apparently this is the convention
});

//DELETE /games/{id} (delete)
routeGroup.MapDelete("/{id}", (int id) =>
{
    Game? game = _games.Find(game => game.Id == id);
    if(game is null)
    {
      //Can return either a 404 or a 204 
      return Results.NotFound();
      //return Results.NoContent();
    }
    _games.Remove(game);
    return Results.NoContent();
});

app.Run();

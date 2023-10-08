using GameCatalogue.Server.Models;
using Microsoft.EntityFrameworkCore;
using GameCatalogue.Server.Data.Configurations;
using GameCatalogue.Server.Data;

/*******************************************************************************/


var builder = WebApplication.CreateBuilder(args);
//configure CORS - cross origin resource sharing / requests
builder.Services.AddCors(options => options.AddDefaultPolicy
  (
    //allow our GameClient to make requests to this server, defined in GameCatalogue.Client -> Properties -> launchSettings.json -> profiles -> applicationUrl
    builder => builder.WithOrigins("http://localhost:5291").AllowAnyHeader().AllowAnyMethod()
  ));
// instead of getting the key from appsettings.json "ConnectionStrings" section, get it from Secret Manager which is run thru readme.
var connString = builder.Configuration.GetConnectionString("GameCatalogueContext");
//inject the GameCatalogueContext Entity Framework as a service
builder.Services.AddSqlServer<GameCatalogueContext>(connString);
//ChatGPT recommends: [ using using Microsoft.EntityFrameworkCore;]
//builder.Services.AddDbContext<GameCatalogueContext>(options => options.UseSqlServer(connString));

var app = builder.Build();
app.UseCors(); //use the CORS policy defined above
string urlRoute = "/games";
string getNameAPIFuncRouteEndpointStr = "GetGame";
var routeGroup = app.MapGroup(urlRoute);
routeGroup.WithParameterValidation(); //Server side validation thru MinimalApis.Extensions which enforces [Required] attribute tags, appllies to all endpoints in this group


/*******************************************************************************/
//POST /games   (create)
routeGroup.MapPost("/", async (GameCatalogueContext context, Game game) =>
{
   context.Games.Add(game); //db auto updates the game.Id
   await context.SaveChangesAsync();
   //return the name of route that can be used to get the new game aka --> GET /games/{id}
   return Results.CreatedAtRoute(getNameAPIFuncRouteEndpointStr, new {id = game.Id} , game);
});

//GET /games (read)
routeGroup.MapGet("/", async (GameCatalogueContext context, string? filter) => 
{
  var games = context.Games.AsNoTracking();
  if(filter is not null)
  {
    games = games.Where(game => game.GameName.Contains(filter) || game.Genre.Contains(filter));
  }
  return await games.ToListAsync();
});

//GET /games/{id} (read)
routeGroup.MapGet("/{id}", async (GameCatalogueContext context, int id) =>
{
   Game? game = await context.Games.FindAsync(id);
   if(game is null)
   {
      return Results.NotFound();
   }
   return Results.Ok(game);
})
.WithName(getNameAPIFuncRouteEndpointStr);


//PUT /games/{id} (update)
routeGroup.MapPut("/{id}",  async (GameCatalogueContext context, int id, Game updatedGame) =>
{

  //Update the existing games fields with the new values
  var rowsAffected = await context.Games.Where(game => game.Id == id)  //oof not the biggest fan of this syntax
  .ExecuteUpdateAsync(updates =>
      updates.SetProperty(game => game.GameName, updatedGame.GameName)
        .SetProperty(game => game.Genre, updatedGame.Genre)
        .SetProperty(game => game.Price, updatedGame.Price)
        .SetProperty(game => game.ReleaseDate, updatedGame.ReleaseDate) );

    if(rowsAffected == 0)
    {
        return Results.NotFound();
        //we can not create a game with a user specified Id, the db auto increments the Id
        // updatedGame.Id  = id;
        // context.Games.Add(updatedGame);
        // await context.SaveChangesAsync();
        // return Results.CreatedAtRoute(getNameAPIFuncRouteEndpointStr, new {id = updatedGame.Id} , updatedGame);
    }
    return Results.NoContent(); 
});

//DELETE /games/{id} (delete)
routeGroup.MapDelete("/{id}", async (GameCatalogueContext context,int id) =>
{
    var rowsAffected = await context.Games.Where(game => game.Id == id).ExecuteDeleteAsync();
    return rowsAffected == 0 ? Results.NotFound() : Results.NoContent();
});

app.Run();

using Microsoft.EntityFrameworkCore;
using GameCatalogue.Server.Models;


namespace GameCatalogue.Server.Data;

  public class GameCatalogueContext : DbContext
  {

      /*****************************************************************************/
      public GameCatalogueContext(DbContextOptions<GameCatalogueContext> options) : base(options)
      {
        
      }
      /*****************************************************************************/
      //property with a getter/setter 
      public DbSet<Game> Games => Set<Game>();

      protected override void OnModelCreating(ModelBuilder modelBuilder)
      {
        //modelBuilder.ApplyConfigurationsFromAssembly(typeof(GameCatalogueContext).Assembly);
        //find every config dynamically 
        modelBuilder.ApplyConfigurationsFromAssembly(System.Reflection.Assembly.GetExecutingAssembly());
      }


  }

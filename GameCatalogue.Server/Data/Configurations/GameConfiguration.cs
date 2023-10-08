using GameCatalogue.Server.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GameCatalogue.Server.Data.Configurations;

  public class GameConfiguration : IEntityTypeConfiguration<Game>
  {
      public void Configure(EntityTypeBuilder<Game> builder)
      {
          builder.Property(g => g.Id).ValueGeneratedOnAdd();
          //builder.Property(g => g.GameName).IsRequired().HasMaxLength(50);
          //builder.Property(g => g.Genre).IsRequired().HasMaxLength(25);
          builder.Property(g => g.Price).HasPrecision(18, 2);
          //builder.Property(g => g.ReleaseDate).IsRequired();
      }
  }

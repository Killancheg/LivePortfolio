using LivePortfolio.Core.Models.DbEntities;
using LivePortfolio.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LivePortfolio.Infrastructure.DbEntities
{
    public class GameConfiguration : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game", "public");

            builder.HasKey(g => g.GameId);

            builder.Property(g => g.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(g => g.CreatorId)
                .IsRequired()
                .HasMaxLength(450);

            builder.HasOne<ApplicationUser>()
                .WithMany()
                .HasForeignKey(g => g.CreatorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(g => g.Reviews)
               .WithOne(r => r.Game)
               .HasForeignKey(r => r.GameId)
               .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

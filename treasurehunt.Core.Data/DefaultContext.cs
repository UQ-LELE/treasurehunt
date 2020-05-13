using Microsoft.EntityFrameworkCore;
using System.Linq;
using treasurehunt.Core.Data.Models.Characters;
using treasurehunt.Core.Data.Models.ItemsOnGame;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.Core.Data
{
    public class DefaultContext : DbContext
    {
        public DefaultContext(DbContextOptions<DefaultContext> options) : base(options)
        {
        }

        protected DefaultContext()
        {
        }

        #region Properties

        
        public DbSet<StoryEvent> StoryEvents { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choice> Choices { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Enemy> Enemies { get; set; }
        public DbSet<ItemOnGame> ItemsOnGame { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<StoryEvent>().ToTable("StoryEvent");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Choice>().ToTable("Choice");
            modelBuilder.Entity<ItemOnGame>().ToTable("ItemOnGame");
            modelBuilder.Entity<Avatar>().ToTable("Avatar");
            modelBuilder.Entity<Hero>().ToTable("Hero");
            modelBuilder.Entity<Enemy>().ToTable("Enemy");

            foreach (var entity in modelBuilder.Model.GetEntityTypes()
        .Where(e => typeof(Character).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entity.Name).Property(nameof(Character.Id))
                    .IsRequired().HasDefaultValueSql("NEWID()");
            }
        }

        #endregion
    }
}

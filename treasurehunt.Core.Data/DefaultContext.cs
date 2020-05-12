using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using treasurehunt.Core.Data.Models;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Personnages;
using treasurehunt.Core.Data.Models.Quetes;

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

        #region Propriétés

        #region Quetes
        public DbSet<Quete> Quetes { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choix> Choixes { get; set; }
        public DbSet<ActionChoix> ActionChoixes { get; set; }
        #endregion

        #region Characters  
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Avatar> Avatars { get; set; }
        public DbSet<Enemy> Enemy { get; set; }
        #endregion

        #region Items
        public DbSet<ItemOnBag> ItemsOnBag { get; set; }
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quete>().ToTable("Quete");
            modelBuilder.Entity<Evenement>().ToTable("Evenement");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Choix>().ToTable("Choix");
            modelBuilder.Entity<ActionChoix>().ToTable("ActionChoix");
            modelBuilder.Entity<Avatar>().ToTable("Avatar");
            modelBuilder.Entity<Hero>().ToTable("Hero");
            modelBuilder.Entity<Enemy>().ToTable("Enemy");
            modelBuilder.Entity<ItemOnBag>().ToTable("ItemOnBag");

            foreach (var entity in modelBuilder.Model.GetEntityTypes()
        .Where(e => typeof(Personnage).IsAssignableFrom(e.ClrType)))
            {
                modelBuilder.Entity(entity.Name).Property(nameof(Personnage.ID))
                    .IsRequired().HasDefaultValueSql("NEWID()");
            }
        }

        #endregion
    }
}

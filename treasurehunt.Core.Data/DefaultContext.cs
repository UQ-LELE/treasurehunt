using Microsoft.EntityFrameworkCore;
using System;
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

        #region Personnages
        public DbSet<Humain> Humains { get; set; }
        public DbSet<Elfe> Elfes { get; set; }
        public DbSet<Nain> Nains { get; set; }
        public DbSet<Rat> Rats { get; set; }
        public DbSet<Spider> Spiders { get; set; }
        public DbSet<Bear> Bears { get; set; }
        public DbSet<Dragon> Dragons { get; set; }
        #endregion

        #region Objets
        public DbSet<ItemOnBag> ItemsOnBag { get; set; }      
        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quete>().ToTable("Quete");
            modelBuilder.Entity<Evenement>().ToTable("Evenement");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Choix>().ToTable("Choix");
            modelBuilder.Entity<ActionChoix>().ToTable("ActionChoix");
            modelBuilder.Entity<Personnage>().ToTable("Personnages");
            modelBuilder.Entity<ItemOnBag>().ToTable("ItemOnBag");
        }

        #endregion
    }
}

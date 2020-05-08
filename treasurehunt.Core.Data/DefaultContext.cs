using Microsoft.EntityFrameworkCore;
using System;
using treasurehunt.Core.Data.Models;
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

        public DbSet<Quete> Quetes { get; set; }
        public DbSet<Evenement> Evenements { get; set; }
        public DbSet<Question> Questions { get; set; }
        public DbSet<Choix> Choixes { get; set; }
        public DbSet<Humain> Humains { get; set; }
        public DbSet<Elfe> Elfes { get; set; }
        public DbSet<Nain> Nains { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quete>().ToTable("Quete");
            modelBuilder.Entity<Evenement>().ToTable("Evenement");
            modelBuilder.Entity<Question>().ToTable("Question");
            modelBuilder.Entity<Choix>().ToTable("Choix");
            modelBuilder.Entity<Personnage>().ToTable("Personnages");
        }

        #endregion
    }
}

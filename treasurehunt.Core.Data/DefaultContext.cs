using Microsoft.EntityFrameworkCore;
using System;
using treasurehunt.Core.Data.Models;
using treasurehunt.Core.Data.Models.Personnages;

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

        public DbSet<Humain> Humains { get; set; }
        public DbSet<Elfe> Elfes { get; set; }
        public DbSet<Nain> Nains { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Personnage>().ToTable("Personnages");
        }

        #endregion
    }
}

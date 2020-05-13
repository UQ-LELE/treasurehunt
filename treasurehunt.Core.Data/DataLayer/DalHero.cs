using System;
using System.Collections.Generic;
using System.Linq;
using treasurehunt.Core.Data.Models.Characters;

namespace treasurehunt.Core.Data.DataLayer
{
    public class DalHero
    {

        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalHero(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Récupérer tous les héros (joueurs)
        /// </summary>

        public List<Hero> GetAllHeroes()
        {
            return this._context.Heroes.ToList();
        }

        /// <summary>
        /// Retourne un héro (joueur)
        /// </summary>
        /// <param name="id">Identifiant du héro recherché</param>
        /// <returns></returns>
        public Hero GetHeroById(Guid id)
        {
            return this._context.Heroes
                                .First(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un héro (joueur)
        /// </summary>
        /// <param name="heroToAdd"></param>
        public void Add(Hero heroToAdd)
        {
            this._context.Heroes.Add(heroToAdd);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Edit et sauvegarde un nouvel héro (joueur)
        /// </summary>
        /// <param name="heroToEdit"></param>
        public void Edit(Hero heroToEdit)
        {
            this._context.Attach<Hero>(heroToEdit);
            this._context.Entry(heroToEdit).Property(item => item).IsModified = true;
            this._context.SaveChanges();
        }

        /// <summary>
        /// Supprime une question
        /// </summary>
        /// <param name="id">Identifiant de la question à supprimer</param>
        public void DeleteHeroById(Guid id)
        {
            Hero heroToDelete = this._context.Heroes.SingleOrDefault(e => e.Id == id);
            if (heroToDelete != null)
            {
                this._context.Heroes.Remove(heroToDelete);
                this._context.SaveChanges();
            }
        }
        #endregion
    }
}

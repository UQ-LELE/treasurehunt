using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<Hero>> GetAll()
        {
            return await this._context.Heroes.ToListAsync();
        }

        /// <summary>
        /// Retourne un héro (joueur)
        /// </summary>
        /// <param name="id">Identifiant du héro recherché</param>
        /// <returns></returns>
        public async Task<Hero> GetById(Guid id)
        {
            return await this._context.Heroes
                                .FirstOrDefaultAsync(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un héro (joueur)
        /// </summary>
        /// <param name="heroToAdd"></param>
        public async Task Add(Hero heroToAdd)
        {
            this._context.Heroes.Add(heroToAdd);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit et sauvegarde un nouvel héro (joueur)
        /// </summary>
        /// <param name="heroToEdit"></param>
        public async Task Edit(Hero heroToEdit)
        {
            this._context.Heroes.Update(heroToEdit);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime un héro
        /// </summary>
        /// <param name="id">Identifiant du héro à supprimer</param>
        public async Task<bool> DeleteById(Guid id)
        {
            bool result = false;

            var heroToDelete = await _context.Heroes
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (heroToDelete != null)
            {
                _context.Entry(heroToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public bool HeroExists(Guid id)
        {
            return _context.Heroes.Any(e => e.Id == id);
        }
        #endregion
    }
}

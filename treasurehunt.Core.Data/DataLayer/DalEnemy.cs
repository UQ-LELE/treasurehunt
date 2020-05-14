using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using treasurehunt.Core.Data.Models.Characters;

namespace treasurehunt.Core.Data.DataLayer
{
    public class DalEnemy
    {

        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalEnemy(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Récupérer tous les énnemis du jeu
        /// </summary>

        public async Task<List<Enemy>> GetAll()
        {
            return await this._context.Enemies.ToListAsync();
        }

        /// <summary>
        /// Retourne un énnemi
        /// </summary>
        /// <param name="id">Identifiant de l'enemi recherchée</param>
        /// <returns></returns>
        public async Task<Enemy> GetById(Guid id)
        {
            return await this._context.Enemies
                                .FirstAsync(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde une nouvelle énnemi
        /// </summary>
        /// <param name="enemyToAdd"></param>
        public async Task Add(Enemy enemyToAdd)
        {
            this._context.Enemies.Add(enemyToAdd);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit et sauvegarde un nouvel énnemi
        /// </summary>
        /// <param name="enemyToEdit"></param>
        public async Task Edit(Enemy enemyToEdit)
        {
            this._context.Attach<Enemy>(enemyToEdit);
            this._context.Entry(enemyToEdit).Property(item => item).IsModified = true;
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime un ennemi
        /// </summary>
        /// <param name="id">Identifiant de l'ennemi à supprimer</param>
        public async Task<bool> DeleteById(Guid id)
        {
            bool result = false;

            var heroToDelete = await _context.Enemies
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
        #endregion
    }
}

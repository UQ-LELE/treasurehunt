using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using treasurehunt.Core.Data.Models.ItemsOnGame;

namespace treasurehunt.Core.Data.DataLayer
{
    public class DalItemOnGame
    {

        #region Fields
        private readonly DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalItemOnGame(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Récupérer tous les objets du jeu
        /// </summary>

        public async Task<List<ItemOnGame>> GetAll() => await this._context.ItemsOnGame.ToListAsync();

        /// <summary>
        /// Retourne un objet
        /// </summary>
        /// <param name="id">Identifiant de l'objet recherché</param>
        /// <returns></returns>
        public async Task<ItemOnGame> GetById(int id)
        {
            return await this._context.ItemsOnGame
                                .FirstOrDefaultAsync(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un nouvel objet
        /// </summary>
        /// <param name="itemToAdd"></param>
        public async Task Add(ItemOnGame itemToAdd)
        {
            this._context.ItemsOnGame.Add(itemToAdd);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit et sauvegarde un nouvel objet
        /// </summary>
        /// <param name="itemToEdit"></param>
        public async Task Edit(ItemOnGame itemToEdit)
        {
            this._context.ItemsOnGame.Update(itemToEdit);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime un objet
        /// </summary>
        /// <param name="id">Identifiant de l'objet à supprimer</param>
        public async Task<bool> DeleteById(int id)
        {
            bool result = false;

            var itemToDelete = await _context.ItemsOnGame
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (itemToDelete != null)
            {
                _context.Entry(itemToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }

        public bool ItemExists(int id)
        {
            return _context.ItemsOnGame.Any(e => e.Id == id);
        }
        #endregion
    }
}

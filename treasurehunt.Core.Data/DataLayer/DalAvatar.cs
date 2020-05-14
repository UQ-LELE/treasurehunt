using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using treasurehunt.Core.Data.Models.Characters;

namespace treasurehunt.Core.Data.DataLayer
{
    public class DalAvatar
    {
        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalAvatar(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Récupérer tous les avatars
        /// </summary>

        public async Task<List<Avatar>> GetAll()
        {
            return await this._context.Avatars
                .ToListAsync();
        }

        /// <summary>
        /// Retourne un avatar
        /// </summary>
        /// <param name="id">Identifiant de l'avatar</param>
        /// <returns></returns>
        public async Task<Avatar> GetById(Guid id)
        {
            return await this._context.Avatars
                                .FirstAsync(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un nouvel avatar
        /// </summary>
        /// <param name="avatarToAdd"></param>
        public async Task Add(Avatar avatarToAdd)
        {
            this._context.Avatars.Add(avatarToAdd);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit et sauvegarde un nouvel avatar
        /// </summary>
        /// <param name="avatarToEdit"></param>
        public async Task Edit(Avatar avatarToEdit)
        {
            this._context.Attach<Avatar>(avatarToEdit);
            this._context.Entry(avatarToEdit).Property(item => item).IsModified = true;
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime un avatar
        /// </summary>
        /// <param name="id">Identifiant de l'avatar à supprimer</param>
        public async Task<bool> DeleteById(Guid id)
        {
            bool result = false;

            var avatarToDelete = await _context.Avatars
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (avatarToDelete != null)
            {
                _context.Entry(avatarToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }
        #endregion
    }
}

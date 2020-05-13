using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<Avatar> GetAllAvatars()
        {
            return this._context.Avatars
                .ToList();
        }

        /// <summary>
        /// Retourne un avatar
        /// </summary>
        /// <param name="id">Identifiant de l'avatar</param>
        /// <returns></returns>
        public Avatar GetAvatarById(Guid id)
        {
            return this._context.Avatars
                                .First(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un nouvel avatar
        /// </summary>
        /// <param name="avatarToAdd"></param>
        public void Add(Avatar avatarToAdd)
        {
            this._context.Avatars.Add(avatarToAdd);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Edit et sauvegarde un nouvel avatar
        /// </summary>
        /// <param name="avatarToEdit"></param>
        public void Edit(Avatar avatarToEdit)
        {
            this._context.Attach<Avatar>(avatarToEdit);
            this._context.Entry(avatarToEdit).Property(item => item).IsModified = true;
            this._context.SaveChanges();
        }

        /// <summary>
        /// Supprime un avatar
        /// </summary>
        /// <param name="id">Identifiant de l'avatar à supprimer</param>
        public void DeleteAvatarById(Guid id)
        {
            Avatar avatarToDelete = this._context.Avatars.SingleOrDefault(e => e.Id == id);
            if (avatarToDelete != null)
            {
                this._context.Avatars.Remove(avatarToDelete);
                this._context.SaveChanges();
            }
        }
        #endregion
    }
}

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.Core.Data.DataLayer
{
    /// <summary>
    /// Layer d'accès à la base de données (encapsule l'appel entities)
    /// </summary>
    public class DalChoice
    {

        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalChoice(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Récupérer tous les choix
        /// </summary>

        public async Task<List<Choice>> GetAll()
        {
            return await this._context.Choices
                .ToListAsync();
        }

        /// <summary>
        /// Retourne un choix
        /// </summary>
        /// <param name="id">Identifiant du choix</param>
        /// <returns></returns>
        public async Task<Choice> GetById(int id)
        {
            return await this._context.Choices
                                .FirstAsync(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un nouveau choix
        /// </summary>
        /// <param name="choiceToAdd"></param>
        public async Task Add(Choice choiceToAdd)
        {
            this._context.Choices.Add(choiceToAdd);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit et sauvegarde un nouveau choix
        /// </summary>
        /// <param name="choiceToEdit"></param>
        public async Task Edit(Choice choiceToEdit)
        {
            this._context.Attach<Choice>(choiceToEdit);
            this._context.Entry(choiceToEdit).Property(item => item).IsModified = true;
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime un choix
        /// </summary>
        /// <param name="id">Identifiant du choix à supprimer</param>
        public async Task<bool> DeleteById(int id)
        {
            bool result = false;

            var choiceToDelete = await _context.Choices
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (choiceToDelete != null)
            {
                _context.Entry(choiceToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }
        #endregion
    }
}

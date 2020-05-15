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
    public class DalQuestion
    {
        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalQuestion(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Récupérer toutes les questions et leur choix associés
        /// </summary>

        public async Task<List<Question>> GetAll()
        {
            return await this._context.Questions
                                .Include(e => e.ChoicesEvent)
                                .ToListAsync();
        }

        /// <summary>
        /// Retourne une question et ses choix associés
        /// </summary>
        /// <param name="id">Identifiant d'un question recherchée</param>
        /// <returns></returns>
        public async Task<Question> GetById(int id)
        {
            return await this._context.Questions
                                .Include(e => e.ChoicesEvent)
                                .FirstOrDefaultAsync(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde une nouvelle question
        /// </summary>
        /// <param name="questionToAdd"></param>
        public async Task Add(Question questionToAdd)
        {
            this._context.Questions.Add(questionToAdd);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit et sauvegarde une nouvelle question
        /// </summary>
        /// <param name="questionToEdit"></param>
        public async Task Edit(Question questionToEdit)
        {
            this._context.Update(questionToEdit);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Supprime une question
        /// </summary>
        /// <param name="id">Identifiant de la question à supprimer</param>
        public async Task<bool> DeleteById(int id)
        {
            bool result = false;

            var questionToDelete = await _context.Questions
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.Id == id);

            if (questionToDelete != null)
            {
                _context.Entry(questionToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }
        #endregion
    }
}

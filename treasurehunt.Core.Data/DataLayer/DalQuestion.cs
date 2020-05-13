using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public List<Question> GetAllQuestion()
        {
            return this._context.Questions
                                .Include(e => e.ChoicesEvent)
                                .ToList();
        }

        /// <summary>
        /// Retourne une question et ses choix associés
        /// </summary>
        /// <param name="id">Identifiant d'un question recherchée</param>
        /// <returns></returns>
        public Question GetQuestionById(int id)
        {
            return this._context.Questions
                                .Include(e => e.ChoicesEvent)
                                .First(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde une nouvelle question
        /// </summary>
        /// <param name="questionToAdd"></param>
        public void Add(Question questionToAdd)
        {
            this._context.Questions.Add(questionToAdd);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Edit et sauvegarde une nouvelle question
        /// </summary>
        /// <param name="questionToEdit"></param>
        public void Edit(Question questionToEdit)
        {
            this._context.Attach<Question>(questionToEdit);
            this._context.Entry(questionToEdit).Property(item => item).IsModified = true;
            this._context.SaveChanges();
        }

        /// <summary>
        /// Supprime une question
        /// </summary>
        /// <param name="id">Identifiant de la question à supprimer</param>
        public void DeleteQuestionById(int id)
        {
            Question questionToDelete = this._context.Questions.SingleOrDefault(e => e.Id == id);
            if (questionToDelete != null)
            {
                this._context.Questions.Remove(questionToDelete);
                this._context.SaveChanges();
            }
        }
        #endregion
    }
}

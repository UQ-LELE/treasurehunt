using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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

        public List<Choice> GetAllChoices()
        {
            return this._context.Choices
                .ToList();
        }

        /// <summary>
        /// Retourne un choix
        /// </summary>
        /// <param name="id">Identifiant du choix</param>
        /// <returns></returns>
        public Choice GetChoiceById(int id)
        {
            return this._context.Choices
                                .First(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un nouveau choix
        /// </summary>
        /// <param name="choiceToAdd"></param>
        public void AddChoice(Choice choiceToAdd)
        {
            this._context.Choices.Add(choiceToAdd);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Edit et sauvegarde un nouveau choix
        /// </summary>
        /// <param name="choiceToEdit"></param>
        public void EditChoice(Choice choiceToEdit)
        {
            this._context.Attach<Choice>(choiceToEdit);
            this._context.Entry(choiceToEdit).Property(item => item).IsModified = true;
            this._context.SaveChanges();
        }

        /// <summary>
        /// Supprime un choix
        /// </summary>
        /// <param name="id">Identifiant du choix à supprimer</param>
        public void DeleteChoiceById(int id)
        {
            Choice choiceToDelete = this._context.Choices.SingleOrDefault(e => e.Id == id);
            if (choiceToDelete != null)
            {
                this._context.Choices.Remove(choiceToDelete);
                this._context.SaveChanges();
            }
        }
        #endregion
    }
}

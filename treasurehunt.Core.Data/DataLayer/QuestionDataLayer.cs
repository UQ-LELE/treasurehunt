using System;
using System.Collections.Generic;
using System.Text;
using treasurehunt.Core.Data.Models.Quetes;

namespace treasurehunt.Core.Data.DataLayer
{
    /// <summary>
    /// Layer d'accès à la base de données (encapsule l'appel entities)
    /// </summary>
    public class QuestionDataLayer
    {
        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public QuestionDataLayer(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods
        /// <summary>
        /// Ajoute et sauvegarde une nouvelle aventure
        /// </summary>
        /// <param name="evenement"></param>
        public void Add(Question question)
        {
            this._context.Questions.Add(question);
            this._context.SaveChanges();
        }

        public void Edit(Question question)
        {
            //edit linq
            this._context.SaveChanges();
        }
        #endregion
    }
}

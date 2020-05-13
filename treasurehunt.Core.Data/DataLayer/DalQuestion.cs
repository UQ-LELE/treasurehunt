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

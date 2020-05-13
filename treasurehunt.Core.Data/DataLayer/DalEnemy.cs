using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public List<Enemy> GetAllEnemies()
        {
            return this._context.Enemies.ToList();
        }

        /// <summary>
        /// Retourne un énnemi
        /// </summary>
        /// <param name="id">Identifiant de l'enemi recherchée</param>
        /// <returns></returns>
        public Enemy GetEnemyById(Guid id)
        {
            return this._context.Enemies
                                .First(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde une nouvelle énnemi
        /// </summary>
        /// <param name="enemyToAdd"></param>
        public void Add(Enemy enemyToAdd)
        {
            this._context.Enemies.Add(enemyToAdd);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Edit et sauvegarde un nouvel énnemi
        /// </summary>
        /// <param name="enemyToEdit"></param>
        public void Edit(Enemy enemyToEdit)
        {
            this._context.Attach<Enemy>(enemyToEdit);
            this._context.Entry(enemyToEdit).Property(item => item).IsModified = true;
            this._context.SaveChanges();
        }

        /// <summary>
        /// Supprime un ennemi
        /// </summary>
        /// <param name="id">Identifiant de la question à supprimer</param>
        public void DeleteQuestionById(Guid id)
        {
            Enemy enemyToDelete = this._context.Enemies.SingleOrDefault(e => e.Id == id);
            if (enemyToDelete != null)
            {
                this._context.Enemies.Remove(enemyToDelete);
                this._context.SaveChanges();
            }
        }
        #endregion
    }
}

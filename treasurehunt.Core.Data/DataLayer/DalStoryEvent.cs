using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.Core.Data.DataLayer
{
    public class DalStoryEvent
    {
        #region Fields
        private DefaultContext _context = null;
        #endregion

        #region Constructors
        public DalStoryEvent(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Retourne tous les évènements, leur question et leur choix associés
        /// </summary>
        /// <returns></returns>
        public List<StoryEvent> GetAllEvent()
        {
            return this._context.StoryEvents
                                .Include(e => e.QuestionEvent)
                                .ThenInclude(e => e.ChoicesEvent)
                                .ToList();
        }

        /// <summary>
        /// Retourne un paragraphe
        /// </summary>
        /// <param name="id">Identifiant du évènement recherché</param>
        /// <returns></returns>
        public StoryEvent GetEventById(int id)
        {
            return this._context.StoryEvents
                                .Include(e => e.QuestionEvent)
                                .ThenInclude(e => e.ChoicesEvent)
                                .First(item => item.Id == id);
        }

        /// <summary>
        /// Ajoute et sauvegarde un nouvelle évènement
        /// </summary>
        /// <param name="eventToAdd"></param>
        public void AddEvent(StoryEvent eventToAdd)
        {
            this._context.StoryEvents.Add(eventToAdd);
            this._context.SaveChanges();
        }

        /// <summary>
        /// Edit et sauvegarde un évènement
        /// </summary>
        /// <param name="eventToEdit">évènement à modifier</param>
        public void EditEvent(StoryEvent eventToEdit)
        {
            this._context.Attach<StoryEvent>(eventToEdit);
            this._context.Entry(eventToEdit).Property(item => item).IsModified = true;
            this._context.SaveChanges();
        }


        /// <summary>
        /// Delete et sauvegarde un évènement
        /// </summary>
        /// <param name="id">Identifiant de l'évènement à supprimer</param>
        public void DeleteEventById(int id)
        {
            StoryEvent eventToDelete = this._context.StoryEvents.SingleOrDefault(e => e.Id == id);
            if (eventToDelete != null)
            {
                this._context.StoryEvents.Remove(eventToDelete);

                this._context.SaveChanges();
            }
        }
        #endregion
    }
}

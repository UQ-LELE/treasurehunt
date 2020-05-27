using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        public DalStoryEvent()
        {
        }
        #endregion

        #region Public methods

        /// <summary>
        /// Retourne tous les évènements, leur question et leur choix associés
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryEvent>> GetAll()
        {
            return await this._context.StoryEvents
                                .Include(e => e.QuestionEvent)
                                .ThenInclude(e => e.ChoicesEvent)
                                .ToListAsync();
        }

        /// <summary>
        /// Retourne un paragraphe
        /// </summary>
        /// <param name="id">Identifiant du évènement recherché</param>
        /// <returns></returns>
        public async Task<StoryEvent> GetById(int id)
        {
            return await this._context.StoryEvents
                                .Include(s => s.QuestionEvent)
                                .ThenInclude(s => s.ChoicesEvent)
                                .FirstOrDefaultAsync(s => s.Id == id);
        }

        /// <summary>
        /// Retourne un enevement selon l'id d'un choix
        /// </summary>
        /// <param name="id">id du choix associé à l'évènement</param>
        /// <returns></returns>
        public async Task<StoryEvent> GetByChoice(int id)
        {

            return await this._context.StoryEvents.Where(s => s.ChoicesEvent.Any(c => c.Id == id))         
                .FirstOrDefaultAsync();
        }

        /// <summary>
        /// Retourne un enevement selon son numéro
        /// </summary>
        /// <param name="id">numéro l'évènement</param>
        /// <returns></returns>
        public async Task<StoryEvent> GetByNumber(string number)
        {

            return await this._context.StoryEvents.Where(s => s.Number == number)
                .Include(s => s.QuestionEvent)
                .ThenInclude(s => s.ChoicesEvent)
                .FirstOrDefaultAsync();
        }

        

        /// <summary>
        /// Ajoute et sauvegarde un nouvelle évènement
        /// </summary>
        /// <param name="eventToAdd"></param>
        public async Task Add(StoryEvent eventToAdd)
        {
            await this._context.StoryEvents.AddAsync(eventToAdd);
            await this._context.SaveChangesAsync();
        }

        /// <summary>
        /// Edit et sauvegarde un évènement
        /// </summary>
        /// <param name="eventToEdit">évènement à modifier</param>
        public async Task Edit(StoryEvent eventToEdit)
        {
            this._context.StoryEvents.Update(eventToEdit);
            await this._context.SaveChangesAsync();
        }


        /// <summary>
        /// Delete et sauvegarde un évènement
        /// </summary>
        /// <param name="id">Identifiant de l'évènement à supprimer</param>
        public async Task<bool> DeleteById(int id)
        {
            bool result = false;

            var eventToDelete = await _context.StoryEvents
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);

            if (eventToDelete != null)
            {
                _context.Entry(eventToDelete).State = EntityState.Deleted;
                await _context.SaveChangesAsync();
                result = true;
            }

            return result;
        }
        #endregion
    }
}

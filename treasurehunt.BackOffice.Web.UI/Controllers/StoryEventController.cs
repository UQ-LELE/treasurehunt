using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class StoryEventController : Controller
    {
        #region Champs privés
        private DalStoryEvent _context = null;
        #endregion

        #region Constructors
        public StoryEventController(DalStoryEvent context)
        {
            this._context = context;
        }
        #endregion

        public IActionResult Index()
        {
            List<StoryEvent> storyEvents = this._context.GetAllEvent();

            return View(storyEvents);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreatePost(StoryEvent eventToCreate)
        {
            IActionResult result = this.View(eventToCreate);

            if (ModelState.IsValid)
            {
                this._context.AddEvent(eventToCreate);
                result = RedirectToAction("Index");
            }

            return result;
        }

        public IActionResult Edit(int id)
        {
            StoryEvent eventToEdit = this._context.GetEventById(id);     

            if(eventToEdit == null)
            {
                return NotFound();
            }

            return View(eventToEdit);
        }

        [HttpPost]
        public IActionResult EditPost(StoryEvent eventToEdit)
        {
            IActionResult result = this.View(eventToEdit);

            if (ModelState.IsValid)
            {
                this._context.EditEvent(eventToEdit);
                result = RedirectToAction("Index");
            }
            return result;
        }
    }
}
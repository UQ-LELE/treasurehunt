using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class StoryEventController : Controller
    {
        #region Champs privés
        private readonly DalStoryEvent _dalStoryEvent = null;
        #endregion

        #region Constructors
        public StoryEventController(DalStoryEvent context)
        {
            this._dalStoryEvent = context;
        }
        #endregion

        // GET ALL : Story Event
        public async Task<IActionResult> Index()
        {

            return View(await this._dalStoryEvent.GetAll());
        }

        //GET : Story Event
        public async Task<IActionResult> Details(int? idStoryEvent)
        {

            StoryEvent storyEvent = null;

            if (idStoryEvent.HasValue)
            {
                storyEvent = await this._dalStoryEvent.GetById(idStoryEvent.Value);
            }
            
            if (storyEvent == null)
            {
                return NotFound();
            }

            return View(storyEvent);
        }

        //CREATE : new Story Event
        public IActionResult Create()
        {
            return View();
        }

        //CREATE POST : new Story Event
        [HttpPost]
        public async Task<IActionResult> CreatePost(StoryEvent eventToCreate)
        {
            IActionResult result = this.View(eventToCreate);

            if (ModelState.IsValid)
            {
                await this._dalStoryEvent.Add(eventToCreate);
                result = RedirectToAction("Index");
            }

            return result;
        }

        //EDIT : Story Event
        public async Task<IActionResult> Edit(int id)
        {
            StoryEvent eventToEdit = await this._dalStoryEvent.GetById(id);     

            if(eventToEdit == null)
            {
                return NotFound();
            }

            return View(eventToEdit);
        }

        //EDIT POST : Story Event
        [HttpPost]
        public async Task<IActionResult> EditPost(StoryEvent eventToEdit)
        {
            IActionResult result = this.View(eventToEdit);

            if (ModelState.IsValid)
            {
                await this._dalStoryEvent.Edit(eventToEdit);
                result = RedirectToAction("Index");
            }
            return result;
        }
    }
}
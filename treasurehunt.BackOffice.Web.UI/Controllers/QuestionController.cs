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
    public class QuestionController : Controller
    {
        #region Champs privés
        private DalQuestion _dalQuestion = null;
        #endregion

        #region Constructors
        public QuestionController(DalQuestion context)
        {
            this._dalQuestion = context;
        }
        #endregion

        public IActionResult Create([FromServices] DalStoryEvent dalStoryEvent, int idStoryEvent)
        {
            ViewBag.EventForQuestion = dalStoryEvent.GetById(idStoryEvent);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost(Question questionForEvent)
        {
            IActionResult result = this.View(questionForEvent);

            if (ModelState.IsValid)
            {
                await this._dalQuestion.Add(questionForEvent);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = questionForEvent.StoryEventId });
            }

            return result;
        }

        public async Task<IActionResult> Edit([FromServices] DalStoryEvent dalStoryEvent, int idQuestion, int idStoryEvent)
        {
            Question questionToEdit = await this._dalQuestion.GetById(idQuestion);

            ViewBag.EventForQuestion = await dalStoryEvent.GetById(idStoryEvent);

            if (questionToEdit == null)
            {
                return NotFound();
            }

            return View(questionToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> EditPost(Question questionToEdit)
        {
            IActionResult result = this.View(questionToEdit);

            if (ModelState.IsValid)
            {
                await this._dalQuestion.Edit(questionToEdit);
                result = RedirectToAction("EventBoard", "StoryEvent", new { id = questionToEdit.StoryEventId });
            }
            return result;
        }
    }
}
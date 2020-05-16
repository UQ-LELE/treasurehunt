using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models.Quest;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class ChoiceController : Controller
    {
        #region Champs privés
        private DalChoice _dalChoice = null;
        #endregion

        #region Constructors
        public ChoiceController(DalChoice context)
        {
            this._dalChoice = context;
        }
        #endregion

        public async Task<IActionResult> Create([FromServices] DalStoryEvent dalStoryEvent, int idStoryEvent)
        {
            ViewBag.EventForChoice = await dalStoryEvent.GetById(idStoryEvent);
            ViewBag.ListOfEvents = await dalStoryEvent.GetAll();

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Choice choiceForQuestion, int idStoryEvent)
        {
            IActionResult result = this.View(choiceForQuestion);

            if (ModelState.IsValid)
            {
                await this._dalChoice.Add(choiceForQuestion);
                result = RedirectToAction("Details", "StoryEvent", new { IdStoryEvent = idStoryEvent });
            }

            return result;
        }

        public async Task<IActionResult> Edit([FromServices] DalStoryEvent dalStoryEvent, int idChoice, int idStoryEvent)
        {
            Choice choiceToEdit = await this._dalChoice.GetById(idChoice);

            ViewBag.EventForChoice = await dalStoryEvent.GetById(idStoryEvent);
            ViewBag.ListOfEvents = await dalStoryEvent.GetAll();

            if (choiceToEdit == null)
            {
                return NotFound();
            }

            return View(choiceToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Choice choiceToEdit, int idStoryEvent)
        {
            IActionResult result = this.View(choiceToEdit);

            if (ModelState.IsValid)
            {
                await this._dalChoice.Edit(choiceToEdit);
                result = RedirectToAction("Details", "StoryEvent", new { IdStoryEvent = idStoryEvent });
            }
            return result;
        }

        // GET: Choices/Delete/5

        public async Task<IActionResult> Delete([FromServices] DalStoryEvent dalStoryEvent, int id, int idStoryEvent)
        {

            var choice = await _dalChoice.GetById(id);
            if (choice == null)
            {
                return NotFound();
            }

            ViewBag.EventOfChoice = await dalStoryEvent.GetById(idStoryEvent);


            return View(choice);
        }

        // POST: Enemies/Delete/5
        [HttpPost, ActionName("DeletePost")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id, int idStoryEvent)
        {
            var enemy = await _dalChoice.DeleteById(id);
            return RedirectToAction("Details", "StoryEvent", new { IdStoryEvent = idStoryEvent });
        }
    }
}

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models.Characters;

namespace treasurehunt.BackOffice.Web.UI.Controllers
{
    public class HeroesController : Controller
    {
        private readonly DalHero _dalHero;

        public HeroesController(DalHero context)
        {
            _dalHero = context;
        }

        // GET: Heroes
        public async Task<IActionResult> Index()
        {
            return View(await _dalHero.GetAll());
        }

        // GET: Heroes/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _dalHero.GetById(id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

       
        // GET: Heroes/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hero = await _dalHero.GetById(id);
            if (hero == null)
            {
                return NotFound();
            }

            return View(hero);
        }

        // POST: Heroes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {

            await _dalHero.DeleteById(id);
            return RedirectToAction(nameof(Index));
        }

        private bool HeroExists(Guid id)
        {
            return _dalHero.HeroExists(id);
        }
    }
}

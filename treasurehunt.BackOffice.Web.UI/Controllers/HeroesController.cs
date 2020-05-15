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

        // GET: Heroes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Heroes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Hero hero, List<IFormFile> Image)
        {
            if (ModelState.IsValid)
            {
                foreach(var item in Image)
                {
                    if (item.Length > 0)
                    {
                        using (var stream = new MemoryStream())
                        {
                            await item.CopyToAsync(stream);
                            hero.Image = stream.ToArray();
                            await _dalHero.Add(hero);
                        }
                    }
                }
              
                return RedirectToAction(nameof(Index));
            }
            return View(hero);
        }

        // GET: Heroes/Edit/5
        public async Task<IActionResult> Edit(Guid id)
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

        // POST: Heroes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("IsPoisoned,Id,Name,Race,Health,Attack,IsDead,IsHero,ImageId")] Hero hero)
        {
            if (id != hero.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
               
                    await _dalHero.Edit(hero);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HeroExists(hero.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
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

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models;
using treasurehunt.Core.Data.Models.Characters;
using treasurehunt.Core.Data.Models.ItemsOnGame;
using treasurehunt.Web.UI.Models;
using treasurehunt.Web.UI.Utilities;

namespace treasurehunt.Web.UI.Controllers
{
    public class HomeController : Controller
    {
       

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Create([FromServices] DalAvatar dalAvatar)
        {
            //send List of Avatar for selecting a hero type
            this.ViewBag.ListOfAvatars = await dalAvatar.GetAll();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([FromServices] DalItemOnGame dalItemOnGame, [FromServices] DalEnemy dalEnemy, [FromServices] DalAvatar dalAvatar, Hero newHero)
        {   
            if (ModelState.IsValid)
            {
                //Instance game parameters with Enemies              
                GameViewModel game = new GameViewModel(await dalEnemy.GetAll());
                
                //Create hero
                Hero hero = new Hero(newHero.Name, newHero.Health, newHero.Attack, newHero.Race);
              
                //save myHero to game parameters
                game.Hero = hero;

                //Get image avatar and save to game parameters
                Avatar avatar = await dalAvatar.GetByRace(hero.Race);
                game.HeroImage = avatar.Image;

                //Game Items from database to game parameters
                game.ItemOnGame = new List<ItemOnGame>();
                game.ItemOnGame = await dalItemOnGame.GetAll();

                //save game parameters to session
                HttpContext.Session.SetComplexObject("Game", game);

                return RedirectToAction("Play", "Game", new { id = 1 });
            }

            //send List of Avatar for selecting a hero type
            this.ViewBag.ListOfAvatars = await dalAvatar.GetAll();

            return View(newHero);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

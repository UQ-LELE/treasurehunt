using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.Models;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Personnages;
using treasurehunt.Core.Data.Models.Quetes;
using treasurehunt.Web.UI.Commons;
using treasurehunt.Web.UI.Utilities;

namespace treasurehunt.Web.UI.Controllers
{
    public class GameController : Controller
    {
        #region vars
        private Rat rat;
        private Dragon dragon;
        private Spider spider;
        private Bear bear;
        private Personnage hero;
        private Evenement evenement;
        private Bag heroBag;
        private ItemOnBag keyTreasure;
        private ItemOnBag dragonTooth;
        private ItemOnBag elixir;
        private ItemOnBag sword;
        private ItemOnBag treasure;
        #endregion

        #region Champs privés
        private DefaultContext _context = null;
        #endregion

        #region Constructeurs
        public GameController(DefaultContext context)
        {
            this._context = context;
        }
        #endregion

        public IActionResult Index()
        {



            return View();
        }

        public IActionResult CreateHero()
        {

            //Objects instance from database to Session
            dragonTooth = this._context.ItemsOnBag.First(item => item.Name == "DragonTooth");
            HttpContext.Session.SetComplexObject("dragonTooth", dragonTooth);

            keyTreasure = this._context.ItemsOnBag.First(item => item.Name == "Key");
            HttpContext.Session.SetComplexObject("keyTreasure", keyTreasure);

            elixir = this._context.ItemsOnBag.First(item => item.Name == "Elixir");
            HttpContext.Session.SetComplexObject("elixir", elixir);

            treasure = this._context.ItemsOnBag.First(item => item.Name == "Treasure");
            HttpContext.Session.SetComplexObject("treasure", treasure);

            sword = this._context.ItemsOnBag.First(item => item.Name == "Sword");
            HttpContext.Session.SetComplexObject("sword", sword);


            //Monsters instance from database to Session
            rat = this._context.Rats.First(pers => pers.Name == "Rat");
            HttpContext.Session.SetComplexObject("rat", rat);

            bear = this._context.Bears.First(pers => pers.Name == "bear");
            HttpContext.Session.SetComplexObject("bear", bear);

            spider = this._context.Spiders.First(pers => pers.Name == "Spider");
            HttpContext.Session.SetComplexObject("spider", spider);

            dragon = this._context.Dragons.First(pers => pers.Name == "Dragon");
            HttpContext.Session.SetComplexObject("dragon", dragon);


            Humain hero = new Humain();
            hero.Name = "Humain";
            hero.Health = 100;
            hero.Attack = 50;
            hero.IsDead = false;
            hero.choisesPath = new List<String>();
            HttpContext.Session.SetComplexObject("Hero", hero);

            heroBag = new Bag();
            heroBag.ItemsOnBag = new List<ItemOnBag>();
            HttpContext.Session.SetComplexObject("HeroBag", heroBag);



            return RedirectToAction("Play", new { choice = "E100" });
        }

        public IActionResult Play(string choice)
        {
            evenement = new Evenement();

            

            ViewBag.Evenement = checkEventStory(getNextEventFromDatabase(choice));
            ViewBag.Question = getQuestionFromDatabase(evenement);
            ViewBag.Choix = getChoixesFromDatabase(evenement);

            addEventToHeroPath(evenement);

            return View();
        }

        private Evenement getNextEventFromDatabase(string choice)
        {
            evenement = new Evenement();
            evenement.Numero = choice;

            if(choice != "EBACK")
            {
                evenement = this._context.Evenements.First(item => item.Numero == choice);
            }

            return evenement;
        }


        private List<Choix> getChoixesFromDatabase(Evenement evenement)
        {
            List<Choix> choixesForThisEvent = this._context.Choixes.Where(choix => choix.EventNumber == evenement.Numero).ToList();
            return choixesForThisEvent;
        }

        private Question getQuestionFromDatabase(Evenement evenement)
        {
            Question questionForThisEvent = this._context.Questions.First(question => question.ID == evenement.QuestionId);
            return questionForThisEvent;
        }

        private Evenement checkEventStory(Evenement evenement)
        {
            hero = HttpContext.Session.GetComplexObject<Humain>("Hero");
            heroBag = HttpContext.Session.GetComplexObject<Bag>("HeroBag");
            rat = HttpContext.Session.GetComplexObject<Rat>("rat");
            spider = HttpContext.Session.GetComplexObject<Spider>("spider");
            bear = HttpContext.Session.GetComplexObject<Bear>("bear");
            dragon = HttpContext.Session.GetComplexObject<Dragon>("dragon");
            keyTreasure = HttpContext.Session.GetComplexObject<ItemOnBag>("keyTreasure");
            dragonTooth = HttpContext.Session.GetComplexObject<ItemOnBag>("dragonTooth");
            sword = HttpContext.Session.GetComplexObject<ItemOnBag>("sword");
            elixir = HttpContext.Session.GetComplexObject<ItemOnBag>("elixir");
            treasure = HttpContext.Session.GetComplexObject<ItemOnBag>("treasure");

            switch (evenement.Numero)
            {
                // case hero want to go back
                case "EBACK":
                    if(getPastEventFromHeroPath() == "E100")
                    {
                        return getNextEventFromDatabase("E001");
                    }
                    return getNextEventFromDatabase(getPastEventFromHeroPath());

                #region Enemy Events
                // case hero go to Rat Event
                case "E321":
                    if (rat.IsDead) { return getNextEventFromDatabase("E321AV"); }
                    else if (hero.choisesPath.Contains("E321")) { return getNextEventFromDatabase("E321V"); }
                    else { return evenement; }

                // case hero go to Bear Event
                case "E432":
                    if (bear.IsDead) { return getNextEventFromDatabase("E432AV"); }
                    else if (hero.choisesPath.Contains("E432")) { return getNextEventFromDatabase("E432D"); }
                    else { return evenement; }

                // case hero go to Dragon Event
                case "E433":
                    if (dragon.IsDead && heroBag.ItemsOnBag.Contains(dragonTooth)) { return getNextEventFromDatabase("E433AV"); }
                    else if (dragon.IsDead && !heroBag.ItemsOnBag.Contains(dragonTooth)) { return getNextEventFromDatabase("E433AVT"); }
                    else { return evenement; }

                // case hero go to Spider Event
                case "E434":
                    if (spider.IsDead) { return getNextEventFromDatabase("E434AV"); }
                    else { return evenement; }
                #endregion

                #region Object Events
                //case hero go to KeyTreasure Event
                case "E322":
                    if (heroBag.ItemsOnBag.Exists(item=>item.Name == "Key")) { return getNextEventFromDatabase("E322V"); }
                    else { return evenement; }
                //case hero go to Sword Event
                case "E323":
                    if (heroBag.ItemsOnBag.Exists(item => item.Name == "Sword")) { return getNextEventFromDatabase("E323V"); }
                    else { return evenement; }
                //case hero go to Elixir Event
                case "E324":
                    if (heroBag.ItemsOnBag.Exists(item => item.Name == "Elixir")) { return getNextEventFromDatabase("E324V"); }
                    else { return evenement; }

                //case hero go to Treasure Event
                case "E544":
                    if (heroBag.ItemsOnBag.Exists(item => item.Name == "Treasure")) { return getNextEventFromDatabase("E544V"); }
                    else { return evenement; }
                #endregion

                #region Taking object Event
                // case taking Key Treasure
                case "E322T":
                    heroBag.ItemsOnBag.Add(keyTreasure);
                    HttpContext.Session.SetComplexObject("HeroBag", heroBag);
                    return evenement;

                    //case taking Sword
                case "E323T":
                    heroBag.ItemsOnBag.Add(sword);
                    HttpContext.Session.SetComplexObject("HeroBag", heroBag);
                    return evenement;

                    //case taking Elixir
                case "E324T":
                    heroBag.ItemsOnBag.Add(elixir);
                    HttpContext.Session.SetComplexObject("HeroBag", heroBag);
                    return evenement;

                    // case taking DragonTooth
                case "E433T":
                    heroBag.ItemsOnBag.Add(dragonTooth);
                    HttpContext.Session.SetComplexObject("HeroBag", heroBag);
                    return evenement;

                    // case taking Treasure
                case "E544T":
                    heroBag.ItemsOnBag.Add(treasure);
                    HttpContext.Session.SetComplexObject("HeroBag", heroBag);
                    return evenement;
                #endregion

                #region Attacks Event
                // case hero Attack rat
                case "E321A":
                    rat.IsDead = true;
                    HttpContext.Session.SetComplexObject("rat", rat);
                    return getNextEventFromDatabase("E321AV");

                // case hero Attack bear
                case "E442A":
                    bear.IsDead = true;
                    HttpContext.Session.SetComplexObject("bear", bear);

                    return getNextEventFromDatabase("E442AV");

                // case hero Attack spider
                case "E434A":
                    spider.IsDead = true;
                    HttpContext.Session.SetComplexObject("spider", spider);

                    return getNextEventFromDatabase("E434AV");

                // case hero Attack dragon
                case "E433A":
                    if (heroBag.ItemsOnBag.Contains(sword)) { 
                        dragon.IsDead = true;
                        HttpContext.Session.SetComplexObject("dragon", dragon);
                        return getNextEventFromDatabase("E433AV"); }
                    else { return getNextEventFromDatabase("E433X"); }

                // case hero Attack vikings
                case "E651A":
                    if(heroBag.ItemsOnBag.Contains(dragonTooth)) { return getNextEventFromDatabase("E651AV"); }
                    else { return getNextEventFromDatabase("E651X"); }

                #endregion


                default:
                    return evenement;
            }
        }

        private void addEventToHeroPath(Evenement evenement)
        {
            // only add general Event with 4 characters : ex: E321 and not E321A
            if(evenement.Numero.Length == 4)
            {
                hero = HttpContext.Session.GetComplexObject<Humain>("Hero");
                hero.choisesPath.Add(evenement.Numero);
                HttpContext.Session.SetComplexObject("Hero", hero);
            }
        }

        private string getPastEventFromHeroPath()
        {
            hero = HttpContext.Session.GetComplexObject<Humain>("Hero");


            // get last event in hero choises path
            string eventNumber = hero.choisesPath[hero.choisesPath.Count - 1];

            // delete last event in hero choises path for good process of BACK in path
            hero.choisesPath.RemoveAt(hero.choisesPath.Count - 1);
            HttpContext.Session.SetComplexObject("Hero", hero);

            return eventNumber;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
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
        #region Champs privés
        private DefaultContext _context = null;
        private Evenement _evenement;
        private Game _game;
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
            //send List of Avatar for selecting a hero type
            this.ViewBag.ListOfAvatars = this._context.Avatars.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult CreateHero(Hero newHero)
        {

            IActionResult result = RedirectToAction("CreateHero");

            if (ModelState.IsValid)
            {
                //Instance game parameters with Enemies
                _game = new Game(this._context.Enemy.ToList());

                //save myHero to game parameters
                Hero hero = new Hero(newHero.Name, newHero.Health, newHero.Attack, newHero.Race);
                _game.Hero = hero;

                //Game Items from database to game parameters
                //_game.ItemOnGame = new List<ItemOnBag>();
                //_game.ItemOnGame = this._context.ItemsOnBag.ToList();

                //save game parameters to session
                HttpContext.Session.SetComplexObject("Game", _game);
                
                result = RedirectToAction("Play", new { choice = "E100" });
            }

            return result;
        }

        public IActionResult Play(string choice)
        {
            // get game parameters from Session
            _game = HttpContext.Session.GetComplexObject<Game>("Game");

            _evenement = new Evenement();

            if (choice == "EBACK")
            {
                //delete actual Event from Hero Path
                _game.Hero.HisPath.RemoveAt(_game.Hero.HisPath.Count - 1);

                // save path in Game session
                HttpContext.Session.SetComplexObject("Game", _game);

                //get previous Event from hero path
                ViewBag.Evenement = checkEventStoryOf(getNexEvent((_game.Hero.HisPath).Last()));
            }
            else
            {
                ViewBag.Evenement = checkEventStoryOf(getNexEvent(choice));
                addEventToHeroPathAndChoice(_evenement);
            }

            ViewBag.Question = getQuestion(_evenement);
            ViewBag.Choix = getChoices(_evenement);
            ViewBag.Hero = _game.Hero;

            return View();
        }

        private Evenement getNexEvent(string choice)
        {
            _evenement = new Evenement();
            _evenement.Numero = choice;

            if (choice != "EBACK")
            {
                _evenement = this._context.Evenements.First(item => item.Numero == choice);
            }

            return _evenement;
        }


        private List<Choix> getChoices(Evenement evenement)
        {
            List<Choix> choixesForThisEvent = this._context.Choixes.Where(choix => choix.EventNumber == evenement.Numero).ToList();
            return choixesForThisEvent;
        }

        private Question getQuestion(Evenement evenement)
        {
            Question questionForThisEvent = this._context.Questions.First(question => question.ID == evenement.QuestionId);
            return questionForThisEvent;
        }

        private Evenement checkEventStoryOf(Evenement evenement)
        {
            //get game parameters from Session
            _game = HttpContext.Session.GetComplexObject<Game>("Game");

            switch (evenement.Numero)
            {
                #region Enemy Events

                // case hero go to Rat Event
                case "E321":
                    if (_game.Rat.IsDead) { return getNexEvent("E321AV"); }
                    else if (_game.Hero.HisChoices.Contains("E321")) { return getNexEvent("E321V"); }
                    else { return evenement; }

                // case hero go to Bear Event
                case "E432":
                    if (!_game.Rat.IsDead && _game.Hero.HisChoices.Last() == "E321")
                    {
                        return checkEventStoryOf(getNexEvent("E321D"));
                    }
                    else if (_game.Bear.IsDead) { return getNexEvent("E432AV"); }
                    else if (_game.Hero.HisChoices.Contains("E432")) { return checkEventStoryOf(getNexEvent("E432D")); }
                    else { return evenement; }

                // case hero go to Dragon Event
                case "E433":
                    if (_game.Dragon.IsDead && _game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { return getNexEvent("E433AV"); }
                    else if (_game.Dragon.IsDead && !_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { return getNexEvent("E433AVT"); }
                    else { return evenement; }

                // case hero go to Spider Event
                case "E434":
                    if (_game.Spider.IsDead && _game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)) { return getNexEvent("E434AV"); }
                    else if (_game.Spider.IsDead && !_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)) { return getNexEvent("E434AVK"); }
                    else { return evenement; }
                #endregion

                #region Object Events
                //case hero go to KeyTreasure Event
                case "E322":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.KEY_TREASURE)) { return getNexEvent("E322V"); }
                    else { return evenement; }
                //case hero go to Sword Event
                case "E323":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.SWORD)) { return getNexEvent("E323V"); }
                    else { return evenement; }
                //case hero go to Elixir Event
                case "E324":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.ELIXIR)) { return getNexEvent("E324V"); }
                    else { return evenement; }

                //case hero go to Treasure Event
                case "E544":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.TREASURE)) { return getNexEvent("E544V"); }
                    else { return evenement; }
                #endregion

                #region Taking object Events
                // case taking Key Treasure
                case "E322T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.KEY_TREASURE));
                    HttpContext.Session.SetComplexObject("Game", _game);
                    return evenement;

                //case taking Sword
                case "E323T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.SWORD));
                    HttpContext.Session.SetComplexObject("Game", _game);
                    return evenement;

                //case taking Elixir
                case "E324T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.ELIXIR));
                    HttpContext.Session.SetComplexObject("Game", _game);
                    return evenement;

                // case taking DragonTooth
                case "E433T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.DRAGON_TOOTH));
                    HttpContext.Session.SetComplexObject("Game", _game);
                    return evenement;

                // case taking Treasure
                case "E544T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.TREASURE));
                    HttpContext.Session.SetComplexObject("Game", _game);
                    return evenement;
                #endregion

                #region Attacks Events
                // case hero Attack rat
                case "E321A":
                    _game.Rat.IsDead = true;
                    HttpContext.Session.SetComplexObject("Game", _game);
                    return getNexEvent("E321AV");

                // case hero Attack bear
                case "E442A":
                    _game.Bear.IsDead = true;
                    HttpContext.Session.SetComplexObject("Game", _game);
                    return getNexEvent("E442AV");

                // case hero Attack spider
                case "E434A":
                    _game.Spider.IsDead = true;
                    HttpContext.Session.SetComplexObject("Game", _game);
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE))
                    {
                        return getNexEvent("E434AV");
                    }
                    else
                    {
                        return getNexEvent("E434AVK");
                    }


                // case hero Attack dragon
                case "E433A":
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.SWORD))
                    {
                        _game.Dragon.IsDead = true;
                        HttpContext.Session.SetComplexObject("Game", _game);
                        return getNexEvent("E433AV");
                    }
                    else { return getNexEvent("E433X"); }

                // case hero Attack vikings
                case "E651A":
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { return getNexEvent("E651AV"); }
                    else { return getNexEvent("E651X"); }

                #endregion

                #region Defense Events
                // case rat attack you
                case "E321D":
                    if (_game.Hero.Health - _game.Rat.Attack < 0)
                    {
                        return getNexEvent("E321X");
                    }
                    else
                    {
                        _game.Rat.IsDead = true;
                        HttpContext.Session.SetComplexObject("Game", _game);
                        _game.Hero.Health -= _game.Rat.Attack;
                        return getNexEvent("E321D");
                    }

                // case Bear attack Hero
                case "E432D":
                    if (_game.Hero.Health - _game.Bear.Attack < 0)
                    {
                        return getNexEvent("E432DX");
                    }
                    else
                    {
                        _game.Bear.IsDead = true;
                        HttpContext.Session.SetComplexObject("Game", _game);
                        _game.Hero.Health -= _game.Bear.Attack;
                        return getNexEvent("E432DV");
                    }

                // case Spider attack Hero

                #endregion

                default:
                    return evenement;
            }
        }

        private void addEventToHeroPathAndChoice(Evenement evenement)
        {

            _game = HttpContext.Session.GetComplexObject<Game>("Game");

            // if starting game, add First Event to Hero Path
            if (_game.Hero.HisChoices.Count == 0)
            {
                _game.Hero.HisPath.Add(evenement.Numero);
            }

            // check if last Hero Path contains same root Event Number
            if (!_game.Hero.HisPath.Last().Contains(evenement.Numero.Substring(0, 4)))
            {
                //if they're not the same, add the root Event Number to the path
                _game.Hero.HisPath.Add(evenement.Numero.Substring(0, 4));
            }

            //anyway add it to Hero Choire to be count at the end of the party
            _game.Hero.HisChoices.Add(evenement.Numero);

            // save new game parameters to session
            HttpContext.Session.SetComplexObject("Game", _game);

        }

        private string getPastEventFromHeroPath()
        {
            _game = HttpContext.Session.GetComplexObject<Game>("Game");
            // get last event in Hero path
            string lastEventNumber = _game.Hero.HisPath.Last();

            return lastEventNumber;
        }
    }
}
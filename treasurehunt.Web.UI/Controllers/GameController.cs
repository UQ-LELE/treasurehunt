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
        private StoryEvent _evenement;
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
                _game.ItemOnGame = new List<ItemOnGame>();
                _game.ItemOnGame = this._context.ItemsOnBag.ToList();

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

            _evenement = new StoryEvent();

            if (choice == "EBACK")
            {
                // delete actual Event from Hero Path
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

        private StoryEvent getNexEvent(string choice)
        {
            _evenement = new StoryEvent();
            _evenement.Numero = choice;

            if (choice != "EBACK")
            {
                _evenement = this._context.Evenements.First(item => item.Numero == choice);
            }

            return _evenement;
        }


        private List<Choice> getChoices(StoryEvent evenement)
        {
            List<Choice> choixesForThisEvent = this._context.Choixes.Where(choix => choix.EventNumber == evenement.Numero).ToList();
            return choixesForThisEvent;
        }

        private Question getQuestion(StoryEvent evenement)
        {
            Question questionForThisEvent = this._context.Questions.First(question => question.ID == evenement.QuestionId);
            return questionForThisEvent;
        }

        private StoryEvent checkEventStoryOf(StoryEvent evenement)
        {
            //get game parameters from Session
            _game = HttpContext.Session.GetComplexObject<Game>("Game");


            switch (evenement.Numero)
            {
                #region Enemy Events

                // case hero go to Rat Event
                case "E321":
                    if (_game.Rat.IsDead) { evenement = getNexEvent("E321AV"); }
                    else if (_game.Hero.HisChoices.Contains("E321")) { evenement = getNexEvent("E321V"); }
                    break;

                // case hero go to Bear Event
                case "E432":
                    if (!_game.Rat.IsDead && _game.Hero.HisChoices.Last() == "E321"){ evenement = checkEventStoryOf(getNexEvent("E321D")); }
                    else if (_game.Bear.IsDead) { evenement = getNexEvent("E432AV"); }
                    else if (_game.Hero.HisChoices.Contains("E432")) { evenement = checkEventStoryOf(getNexEvent("E432D")); }
                    break;

                // case hero go to Dragon Event
                case "E433":
                    if (_game.Dragon.IsDead && _game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { evenement = getNexEvent("E433AV"); }
                    else if (_game.Dragon.IsDead && !_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { evenement = getNexEvent("E433AVT"); }
                    break;

                // case hero go to Spider Event
                case "E434":
                    if (_game.Spider.IsDead && _game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)) { evenement = getNexEvent("E434AV"); }
                    else if (_game.Spider.IsDead && !_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)) { evenement = getNexEvent("E434AVK"); }
                    break;

                #endregion

                #region Object Events
                //case hero go to KeyTreasure Event
                case "E322":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.KEY_TREASURE)) { evenement = getNexEvent("E322V"); }
                    break;

                //case hero go to Sword Event
                case "E323":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.SWORD)) { evenement = getNexEvent("E323V"); }
                    break;

                //case hero go to Elixir Event
                case "E324":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.ELIXIR)) { evenement = getNexEvent("E324V"); }
                    break;

                //case hero go to Treasure Event
                case "E544":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.TREASURE)) { evenement = getNexEvent("E544V"); }
                    break;

                #endregion

                #region Taking object Events
                // case taking Key Treasure
                case "E322T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.KEY_TREASURE));
                    break;

                //case taking Sword
                case "E323T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.SWORD));
                    break;

                //case taking Elixir
                case "E324T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.ELIXIR));
                    break;

                // case taking DragonTooth
                case "E433T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.DRAGON_TOOTH));
                    break;

                // case taking Treasure
                case "E544T":
                    _game.Hero.ItemsOnBag.Add(_game.ItemOnGame.Find(item => item.Name == GameConst.TREASURE));
                    break;
                    #endregion

                #region Attacks Events
                // case hero Attack rat
                case "E321A":
                    _game.Rat.IsDead = true;
                    evenement = getNexEvent("E321AV");
                    break;

                // case hero Attack bear
                case "E432A":
                    _game.Bear.IsDead = true;
                    evenement = getNexEvent("E432AV");
                    break;

                // case hero Attack spider
                case "E434A":
                    _game.Spider.IsDead = true;
                    _game.Hero.IsPoisoned = true;
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)){ evenement = getNexEvent("E434AV");}
                    else {evenement = getNexEvent("E434AVK");}
                    break;

                // case hero Attack dragon
                case "E433A":
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.SWORD))
                    {
                        _game.Dragon.IsDead = true;
                        evenement = getNexEvent("E433AV");
                    }
                    else { _game.Hero.IsDead = true; evenement = getNexEvent("E433X"); }
                    break;

                // case hero Attack vikings
                case "E651A":
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { evenement = getNexEvent("E651AV"); }
                    else { _game.Hero.IsDead = true; evenement = getNexEvent("E651X"); }
                    break;
                #endregion

                #region Defense Events
                // case rat attack you
                case "E321D":
                    if (_game.Hero.Health - _game.Rat.Attack < 0) { _game.Hero.IsDead = true; evenement = getNexEvent("E321X");}
                    else
                    {
                        _game.Rat.IsDead = true;
                        _game.Hero.Health -= _game.Rat.Attack;
                        evenement = getNexEvent("E321D");
                    }
                    break;
                // case Bear attack Hero
                case "E432D":
                    if (_game.Hero.Health - _game.Bear.Attack < 0){ _game.Hero.IsDead = true;  evenement = getNexEvent("E432DX");}
                    else
                    {
                        _game.Bear.IsDead = true;
                        _game.Hero.Health -= _game.Bear.Attack;
                        evenement = getNexEvent("E432DV");
                    }
                    break;
                #endregion

                #region Endings Events
                case "761":
                    if(!_game.Hero.ItemsOnBag.Any(items=>items.Name == GameConst.TREASURE)) { evenement = getNexEvent("E761X"); }
                    break;
                #endregion

                default:
                    break;
            }

            //if Hero is poisonned by spider, hit him on eatch step of path and verify if he's dead
            if (_game.Hero.IsPoisoned)
            {
                _game.Hero.Health -= 10;
                if(_game.Hero.Health <= 0) { evenement = getNexEvent("EXXX"); }          
            }

            //save new Game parameters in session
            HttpContext.Session.SetComplexObject("Game", _game);

            return evenement;
        }

        private void addEventToHeroPathAndChoice(StoryEvent evenement)
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
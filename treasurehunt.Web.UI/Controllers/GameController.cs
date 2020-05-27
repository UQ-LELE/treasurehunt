using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using treasurehunt.Core.Data;
using treasurehunt.Core.Data.DataLayer;
using treasurehunt.Core.Data.Models;
using treasurehunt.Core.Data.Models.Characters;
using treasurehunt.Core.Data.Models.ItemsOnGame;
using treasurehunt.Core.Data.Models.Quest;
using treasurehunt.Web.UI.Commons;
using treasurehunt.Web.UI.Utilities;

namespace treasurehunt.Web.UI.Controllers
{
    public class GameController : Controller
    {
        #region Champs privés
        private DalStoryEvent _dalStoryEvent = null;
        private StoryEvent _evenement;
        private GameViewModel _game;
        #endregion

        #region Constructeurs
        public GameController(DalStoryEvent context)
        {
            this._dalStoryEvent = context;
        }
        #endregion

        public IActionResult Index()
        {
            return View();
        }

       

        public async Task<IActionResult> Play(int? id, string? back)
        {
            // get game parameters from Session
            _game = HttpContext.Session.GetComplexObject<GameViewModel>("Game");


            if (back != null && back == "EBACK")
            {
                // delete actual Event from Hero Path
                _game.Hero.HisPath.RemoveAt(_game.Hero.HisPath.Count - 1);

                // save path in Game session
                HttpContext.Session.SetComplexObject("Game", _game);

                //get previous Event from hero path
                _evenement = await _dalStoryEvent.GetByNumber((_game.Hero.HisPath).Last());
                
                //check event in story
                _evenement = await CheckEventStoryOf(_evenement);
            }
            else if(id.HasValue)
            {
                _evenement = await _dalStoryEvent.GetById(id.Value);
                _evenement = await CheckEventStoryOf(_evenement);
                AddEventToHeroPathAndChoice(_evenement);
            }else
            {
                return NotFound();
            }

            _game.StoryEvent = _evenement;
            HttpContext.Session.SetComplexObject("Game", _game);

            return View(_game);
        }

        private async Task<StoryEvent> GetNexEvent(string choice)
        {
           
            if (choice != "EBACK")
            {
                _evenement = await _dalStoryEvent.GetByNumber(choice);

            }

            return _evenement;
        }


        private async Task<StoryEvent> CheckEventStoryOf(StoryEvent evenement)
        {
            //get game parameters from Session
            _game = HttpContext.Session.GetComplexObject<GameViewModel>("Game");


            switch (evenement.Number)
            {
                #region Enemy Events

                // case hero go to Rat Event
                case "E321":
                    if (_game.Rat.IsDead) { evenement = await GetNexEvent("E321AV"); }
                    else if (_game.Hero.HisChoices.Contains("E321")) { evenement = await GetNexEvent("E321V"); }
                    break;

                // case hero go to Bear Event
                case "E432":
                    if (!_game.Rat.IsDead && _game.Hero.HisChoices.Last() == "E321"){ evenement = await CheckEventStoryOf(await GetNexEvent("E321D")); }
                    else if (_game.Bear.IsDead) { evenement = await GetNexEvent("E432AV"); }
                    else if (_game.Hero.HisChoices.Contains("E432")) { evenement = await CheckEventStoryOf(await GetNexEvent("E432D")); }
                    break;

                // case hero go to Dragon Event
                case "E433":
                    if (_game.Dragon.IsDead && _game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { evenement = await GetNexEvent("E433AV"); }
                    else if (_game.Dragon.IsDead && !_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { evenement = await GetNexEvent("E433AVT"); }
                    break;

                // case hero go to Spider Event
                case "E434":
                    if (_game.Spider.IsDead && _game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)) { evenement = await GetNexEvent("E434AV"); }
                    else if (_game.Spider.IsDead && !_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)) { evenement = await  GetNexEvent("E434AVK"); }
                    break;

                #endregion

                #region Object Events
                //case hero go to KeyTreasure Event
                case "E322":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.KEY_TREASURE)) { evenement = await GetNexEvent("E322V"); }
                    break;

                //case hero go to Sword Event
                case "E323":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.SWORD)) { evenement = await GetNexEvent("E323V"); }
                    break;

                //case hero go to Elixir Event
                case "E324":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.ELIXIR)) { evenement = await GetNexEvent("E324V"); }
                    break;

                //case hero go to Treasure Event
                case "E544":
                    if (_game.Hero.ItemsOnBag.Exists(item => item.Name == GameConst.TREASURE)) { evenement = await GetNexEvent("E544V"); }
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
                    evenement = await GetNexEvent("E321AV");
                    break;

                // case hero Attack bear
                case "E432A":
                    _game.Bear.IsDead = true;
                    evenement = await GetNexEvent("E432AV");
                    break;

                // case hero Attack spider
                case "E434A":
                    _game.Spider.IsDead = true;
                    _game.Hero.IsPoisoned = true;
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.KEY_TREASURE)){ evenement = await GetNexEvent("E434AV");}
                    else {evenement = await GetNexEvent("E434AVK");}
                    break;

                // case hero Attack dragon
                case "E433A":
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.SWORD))
                    {
                        _game.Dragon.IsDead = true;
                        evenement = await GetNexEvent("E433AVT");
                    }
                    else { _game.Hero.IsDead = true; evenement = await GetNexEvent("E433X"); }
                    break;

                // case hero Attack vikings
                case "E651A":
                    if (_game.Hero.ItemsOnBag.Any(item => item.Name == GameConst.DRAGON_TOOTH)) { evenement = await GetNexEvent("E651AV"); }
                    else { _game.Hero.IsDead = true; evenement = await GetNexEvent("E651X"); }
                    break;
                #endregion

                #region Defense Events
                // case rat attack you
                case "E321D":
                    if (_game.Hero.Health - _game.Rat.Attack < 0) { _game.Hero.IsDead = true; evenement = await GetNexEvent("E321X");}
                    else
                    {
                        _game.Rat.IsDead = true;
                        _game.Hero.Health -= _game.Rat.Attack;
                        evenement = await GetNexEvent("E321D");
                    }
                    break;
                // case Bear attack Hero
                case "E432D":
                    if (_game.Hero.Health - _game.Bear.Attack < 0){ _game.Hero.IsDead = true;  evenement = await GetNexEvent("E432DX");}
                    else
                    {
                        _game.Bear.IsDead = true;
                        _game.Hero.Health -= _game.Bear.Attack;
                        evenement = await GetNexEvent("E432DV");
                    }
                    break;
                #endregion

                #region Endings Events
                case "761":
                    if(!_game.Hero.ItemsOnBag.Any(items=>items.Name == GameConst.TREASURE)) { evenement = await GetNexEvent("E761X"); }
                    break;
                #endregion

                default:

                    break;
            }

            //if Hero is poisonned by spider, hit him on eatch step of path and verify if he's dead
            if (_game.Hero.IsPoisoned)
            {
                _game.Hero.Health -= 5;
                if(_game.Hero.Health <= 0) { evenement = await GetNexEvent("EXXX"); }          
            }

            //save new Game parameters in session
            HttpContext.Session.SetComplexObject("Game", _game);

            return evenement;
        }

        private void AddEventToHeroPathAndChoice(StoryEvent evenement)
        {

            _game = HttpContext.Session.GetComplexObject<GameViewModel>("Game");

            // if starting game, add First Event to Hero Path
            if (_game.Hero.HisChoices.Count == 0)
            {
                _game.Hero.HisPath.Add(evenement.Number);
            }

            // check if last Hero Path contains same root Event Number
            if (!_game.Hero.HisPath.Last().Contains(evenement.Number.Substring(0, 4)))
            {
                //if they're not the same, add the root Event Number to the path
                _game.Hero.HisPath.Add(evenement.Number.Substring(0, 4));
            }

            //anyway add it to Hero Choire to be count at the end of the party
            _game.Hero.HisChoices.Add(evenement.Number);

            // save new game parameters to session
            HttpContext.Session.SetComplexObject("Game", _game);

        }

    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace treasurehunt.Web.UI.Commons
{
    public static class EventConst
    {

        #region Events 100
        public const string Event100 = "E100"; //Entrance of the cave
        #endregion

        #region Events 211
        public const string Event211 = "E211"; //The is a dead body empty
        #endregion

        #region Events 212
        public const string Event212 = "E212"; //The is a dead body with paper
        #endregion

        #region Events 321       
        public const string Event321 = "E321"; //There is a rat
        public const string Event321A = "E321A"; //You attack and kill the rat
        public const string Event321AV = "E321AV"; //The rat is dead
        public const string Event321D = "E321D"; //The rat attack you and run
        public const string Event321V = "E321V"; //The is no more rat
        public const string Event321X = "E321X"; //The rat kill you
        #endregion

        #region Events 322
        public const string Event322 = "E322"; //There is a key
        public const string Event322T = "E322T"; //You take the key
        public const string Event322V = "E322V"; //There is no key
        #endregion

        #region Events 323
        public const string Event323 = "E323"; //There is a sword
        public const string Event323T = "E323T"; //You take the sword
        public const string Event323V = "E323V"; //There is no sword
        #endregion

        #region Events 324
        public const string Event324 = "E324"; //There is an Elixir
        public const string Event324T = "E324T"; //You take the Elixir
        public const string Event324V = "E324V"; //There is no Elixir
        #endregion

        #region Events 431
        public const string Event431 = "E431"; //You die in a hole
        #endregion

        #region Events 432
        public const string Event432 = "E432"; //There is a bear sleeping
        public const string Event432A = "E432A"; //You attack the bear
        public const string Event431AV = "E432AV"; //The bear is dead
        public const string Event431D = "E432D"; //The bear attack you
        public const string Event431X = "E432X"; //The bear kill you
        #endregion

        #region Events 433
        public const string Event433 = "E433"; //There is a Dragon
        public const string Event433A = "E433A"; //You attack the Dragon
        public const string Event433AVT = "E433AVT"; //The Dragon is dead and you can take the tooth
        public const string Event433AV = "E433AV"; //The Dragon is dead
        public const string Event433X = "E433X"; //The Dragon kill you
        public const string EventE433T = "E433T"; //You take the tooth dragon
        #endregion

        #region Events 434
        public const string Event434 = "E434"; //There is a spider
        public const string Event434A = "E434A"; //You attack the Spider
        public const string Event434AV = "E434AV"; //You kill the spider
        public const string Event434X = "E434X"; //The spider kill you
        #endregion

        #region Events 435
        public const string Event435 = "E435X"; //You die in a hole
        #endregion


        #region Events 541
        public const string Event541 = "E541X"; //You die in a hole
        #endregion

        #region Events 542
        public const string Event542 = "E542"; //The is a dead body with paper
        #endregion

        #region Events 543
        public const string Event543 = "E543"; //The is a dead body empty
        #endregion

        #region Events 544
        public const string Event544 = "E544"; //The is a teasure
        public const string Event544T = "E544T"; //You take the teasure
        public const string Event544V = "E544V"; //The is no more teasure
        #endregion

        #region Events 651
        public const string Event651 = "E651"; //There's a lot of Vikings
        public const string Event651A = "E651A"; //You attack the Vikings
        public const string Event651AV = "E651AV"; //You affraid all the Vikings
        public const string Event651X = "E651X"; //Vikings kill you
        #endregion

        #region Events 761
        public const string Event761 = "E761"; //You win
        public const string Event761X = "E761X"; //You loose, no treasure, shame !
        #endregion
    }
}

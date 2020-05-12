using System;
using System.Collections.Generic;
using System.Text;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Personnages;

namespace treasurehunt.Core.Data.Models
{
    public class Game
    {
       
        public Enemy Rat { get; set; }
        
        public Enemy Dragon { get; set; }
        
        public Enemy Bear { get; set; }
        
        public Enemy Spider { get; set; }
        public Game() { }
        public Game (List<Enemy> enemies)
        {
            
            Rat = enemies.Find(enemy => enemy.Race == "Rat");
            Dragon = enemies.Find(enemy => enemy.Race == "Dragon");
            Bear = enemies.Find(enemy => enemy.Race == "Bear");
            Spider = enemies.Find(enemy => enemy.Race == "Spider");
        }

        public Hero Hero { get; set; }
    
        public List<ItemOnBag> ItemOnGame { get; set; } 
    }
}

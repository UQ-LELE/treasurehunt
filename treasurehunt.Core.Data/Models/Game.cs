using System;
using System.Collections.Generic;
using System.Text;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Personnages;

namespace treasurehunt.Core.Data.Models
{
    public class Game
    {
       
        public Rat Rat { get; set; }
        
        public Dragon Dragon { get; set; }
        
        public Bear Bear { get; set; }
        
        public Spider Spider { get; set; }
        public Game() { }
        public Game (List<Personnage> enemies)
        {
            Rat = (Rat)enemies.Find(enemy => enemy.Name == "Rat");
            Dragon = (Dragon)enemies.Find(enemy => enemy.Name == "Dragon");
            Bear = (Bear)enemies.Find(enemy => enemy.Name == "Bear");
            Spider = (Spider)enemies.Find(enemy => enemy.Name == "Spider");
        }

        public Hero Hero { get; set; }
    
        public List<ItemOnBag> ItemOnGame { get; set; } 
    }
}

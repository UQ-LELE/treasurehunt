using System;
using System.Security.Cryptography.X509Certificates;
using treasurehunt.Core.Data.Models;
using treasurehunt.Core.Data.Models.Objets;
using treasurehunt.Core.Data.Models.Personnages;
using treasurehunt.Core.Data.Models.Quetes;

namespace ScriptQuest
{
    class Program
    {

        static void Main(string[] args)
        {
            Evenement evenement = new Evenement();


        }

        private static void questEvenement(Evenement evenement, Rat rat, Dragon dragon, Spider spider, Bear bear, Personnage personnage, ItemOnBag itemOnBag)
        {
            switch (evenement.Numero)
            {
                case "E100":
                    Console.WriteLine(evenement.Numero);
                    break;
                case "E211":
                    Console.WriteLine(evenement.Numero);
                    break;
                case "E212":
                    Console.WriteLine(evenement.Numero);
                    break;
                case "E321":
                    if (rat.IsDead)
                    {
                        Console.WriteLine("E321AV");
                    }
                    else if (personnage.choisesPath.Contains(evenement.Numero))
                    {
                        Console.WriteLine("E321AV");
                    }
                    else
                    {
                        Console.WriteLine("E321");
                    }
                    break;
                case "E322":
                    if()
                    break;
                case "E323":
                    break;
                case "E324":
                    break;
                case "E431":
                    break;
                case "E432":
                    break;
                case "E433":
                    break;
                case "E434":
                    break;
                case "E435":
                    break;
                case "E541":
                    break;
                case "E542":
                    break;
                case "E543":
                    break;
                case "E544":
                    break;
                case "E651":
                    break;
                case "E761":
                    break;

            }

        }

    }
}

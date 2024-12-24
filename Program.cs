using TP_Final.Models.Characters;
using TP_Final.Models.Combat;
using TP_Final.Utils;
using TP_Final.Models;

namespace TP_Final
{
    class Program
    {
        static void Main(string[] args)
        {
            // Sélection des personnages
            Console.WriteLine("Bienvenue dans le jeu J-RPG !");
            var player1 = ChooseCharacter("Joueur 1");
            var player2 = ChooseCharacter("Joueur 2");

            // Lancement du combat
            var combatManager = new CombatManager(player1, player2);
            combatManager.StartCombat();
        }

        static Character ChooseCharacter(string playerName)
        {
            var classes = new Dictionary<string, string>
            {
                {"1", "Guerrier"},
                {"2", "Mage"},
                {"3", "Paladin"},
                {"4", "Voleur"}
                
                // Ajoutez ici d'autres classes comme Voleur, Paladin, etc.
            };

            string choice = PromptChoice.GetChoice($"Choisissez une classe pour {playerName} :", classes);

            return choice switch
            {
                "1" => new Warrior($"{playerName} (Guerrier)"),
                "2" => new Mage($"{playerName} (Mage)"),
                "3" => new Paladin($"{playerName} (Paladin)"),
                "4" => new Rogue($"{playerName} (Voleur)"),
                _ => throw new InvalidOperationException("Classe non valide.") // Impossible normalement
            };
        }
    }
}
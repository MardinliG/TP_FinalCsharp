using TP_Final.Models.Enums;
using TP_Final.Models.Combat;
using TP_Final.Utils;

namespace TP_Final.Models.Characters
{
    public class Rogue : Character
    {
        private bool EvasionActive = false;

        public Rogue(string name) : base(
            name,
            health: 80,
            physicalAttack: 55,
            magicalAttack: 0,
            ArmorType.Leather,
            dodgeChance: 0.15,
            parryChance: 0.25,
            spellResistance: 0.25)
        {
        }

        public void CheapShot(Character target)
        {
            double multiplier = target.CurrentHealth < (target.MaxHealth / 2) ? 1.5 : 1.0;
            double damage = PhysicalAttackPower * multiplier;
            Console.WriteLine($"{Name} utilise Coup bas sur {target.Name} !");
            target.Defend(new Attack(this, damage, DamageType.Physical, "Coup bas"));
        }

        public void Evasion()
        {
            if (!EvasionActive)
            {
                DodgeChance = Math.Min(DodgeChance + 0.2, 0.5);
                SpellResistanceChance = Math.Min(SpellResistanceChance + 0.2, 0.5);
                EvasionActive = true;
                Console.WriteLine($"{Name} active Évasion, augmentant ses chances d'esquive et de résistance !");
            }
            else
            {
                Console.WriteLine($"{Name} est déjà en mode Évasion !");
            }
        }

        public override void ChooseAction(Character target)
        {
            var actions = new Dictionary<string, string>
            {
                {"1", "Attaque basique"},
                {"2", "Coup bas"},
                {"3", "Évasion"}
            };

            string choice = PromptChoice.GetChoice($"Tour de {Name}. Choisissez une action :", actions);

            switch (choice)
            {
                case "1":
                    Attack(target);
                    break;
                case "2":
                    CheapShot(target);
                    break;
                case "3":
                    Evasion();
                    break;
            }
        }
    }
}

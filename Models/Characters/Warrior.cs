using TP_Final.Models.Enums;
using TP_Final.Models.Combat;
using TP_Final.Utils;

namespace TP_Final.Models.Characters
{
    public class Warrior : Character
    {
        private bool BattleCryActive = false;
        private double BasePhysicalAttackPower;

        public Warrior(string name) : base(
            name,
            health: 100,
            physicalAttack: 50,
            magicalAttack: 0,
            ArmorType.Plate,
            dodgeChance: 0.05,
            parryChance: 0.25,
            spellResistance: 0.10)
        {
            BasePhysicalAttackPower = PhysicalAttackPower;
        }

        public void HeroicStrike(Character target)
        {
            Console.WriteLine($"{Name} utilise Frappe héroïque sur {target.Name} !");
            target.Defend(new Attack(this, PhysicalAttackPower, DamageType.Physical, "Frappe héroïque"));
        }

        public void BattleCry()
        {
            if (!BattleCryActive)
            {
                PhysicalAttackPower = BasePhysicalAttackPower * 2;
                BattleCryActive = true;
                Console.WriteLine($"{Name} pousse un cri de bataille ! Sa puissance d'attaque est doublée !");
            }
            else
            {
                Console.WriteLine($"{Name} est déjà sous l'effet du cri de bataille !");
            }
        }

        public override void Defend(Attack attack)
        {
            base.Defend(attack);

            if (!IsDead && attack.DamageType == DamageType.Physical && Random.Shared.NextDouble() < 0.25)
            {
                Console.WriteLine($"{Name} contre-attaque !");
                attack.Attacker.Defend(new Attack(this, attack.Damage * 0.5, DamageType.Physical, "Contre-attaque"));
            }
        }

        public override void Heal(double amount)
        {
            if (IsDead)
            {
                Console.WriteLine($"{Name} ne peut pas se soigner car il est mort.");
                return;
            }

            double actualHeal = Math.Min(MaxHealth - CurrentHealth, amount * 1.2); // Guerrier soigne 20% de plus
            CurrentHealth += actualHeal;
            Console.WriteLine($"{Name} utilise son endurance pour récupérer {actualHeal:F1} PV. PV actuels : {CurrentHealth:F1}/{MaxHealth}");
        }

        public override void ChooseAction(Character target)
        {
            var actions = new Dictionary<string, string>
            {
                {"1", "Attaque basique"},
                {"2", "Frappe héroïque"},
                {"3", "Cri de bataille"},
                {"4", "Se soigner"}
            };

            string choice = PromptChoice.GetChoice($"Tour de {Name}. Choisissez une action :", actions);

            switch (choice)
            {
                case "1":
                    Attack(target);
                    break;
                case "2":
                    HeroicStrike(target);
                    break;
                case "3":
                    BattleCry();
                    break;
                case "4":
                    Heal(20); 
                    break;
            }
        }
    }
}
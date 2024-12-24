using TP_Final.Models.Enums;
using TP_Final.Models.Combat;
using TP_Final.Utils;

namespace TP_Final.Models.Characters
{
    public class Paladin : Character
    {
        public Paladin(string name) : base(
            name,
            health: 95,
            physicalAttack: 40,
            magicalAttack: 40,
            ArmorType.Mail,
            dodgeChance: 0.05,
            parryChance: 0.10,
            spellResistance: 0.20)
        {
        }

        public void CrusaderStrike(Character target)
        {
            Console.WriteLine($"{Name} utilise Frappe du croisé sur {target.Name} !");
            var attack = new Attack(this, PhysicalAttackPower, DamageType.Physical, "Frappe du croisé");
            target.Defend(attack);
            HealFromDamage(attack.Damage);
        }

        public void Judgment(Character target)
        {
            Console.WriteLine($"{Name} utilise Jugement sur {target.Name} !");
            var attack = new Attack(this, MagicalAttackPower, DamageType.Magical, "Jugement");
            target.Defend(attack);
            HealFromDamage(attack.Damage);
        }

        public void HolyLight()
        {
            double healAmount = MagicalAttackPower * 1.25;
            Console.WriteLine($"{Name} lance Éclair lumineux !");
            Heal(healAmount);
        }

        private void HealFromDamage(double damage)
        {
            double healAmount = damage * 0.5;
            Heal(healAmount);
        }

        public override void ChooseAction(Character target)
        {
            var actions = new Dictionary<string, string>
            {
                {"1", "Attaque basique"},
                {"2", "Frappe du croisé"},
                {"3", "Jugement"},
                {"4", "Éclair lumineux"}
            };

            string choice = PromptChoice.GetChoice($"Tour de {Name}. Choisissez une action :", actions);

            switch (choice)
            {
                case "1":
                    Attack(target);
                    break;
                case "2":
                    CrusaderStrike(target);
                    break;
                case "3":
                    Judgment(target);
                    break;
                case "4":
                    HolyLight();
                    break;
            }
        }
    }
}
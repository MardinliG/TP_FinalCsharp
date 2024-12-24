using TP_Final.Models.Enums;
using TP_Final.Models.Combat;
using TP_Final.Utils;

namespace TP_Final.Models.Characters
{
    public class Mage : Character
    {
        private int FrostBarrierCharges = 0;

        public Mage(string name) : base(
            name,
            health: 60,
            physicalAttack: 0,
            magicalAttack: 75,
            ArmorType.Cloth,
            dodgeChance: 0.05,
            parryChance: 0.05,
            spellResistance: 0.25)
        {
        }

        public void FrostBolt(Character target)
        {
            Console.WriteLine($"{Name} lance un Éclair de givre sur {target.Name} !");
            target.Defend(new Attack(this, MagicalAttackPower, DamageType.Magical, "Éclair de givre"));
        }

        public void FrostBarrier()
        {
            FrostBarrierCharges = 2;
            Console.WriteLine($"{Name} crée une barrière de givre !");
        }

        protected override double CalculateDamage(Attack attack)
        {
            double damage = base.CalculateDamage(attack);

            if (FrostBarrierCharges > 0)
            {
                damage *= attack.DamageType == DamageType.Physical ? 0.4 : 0.5;
                FrostBarrierCharges--;
                Console.WriteLine($"La barrière de givre absorbe une partie des dégâts ! ({FrostBarrierCharges} charges restantes)");
            }

            return damage;
        }

        public void ArcaneHeal()
        {
            double healAmount = MagicalAttackPower * 1.5; // Le mage soigne plus efficacement
            Console.WriteLine($"{Name} utilise un sort de soin arcanique !");
            Heal(healAmount);
        }

        public override void ChooseAction(Character target)
        {
            var actions = new Dictionary<string, string>
            {
                {"1", "Attaque basique"},
                {"2", "Éclair de givre"},
                {"3", "Barrière de givre"},
                {"4", "Sort de soin"}
            };

            string choice = PromptChoice.GetChoice($"Tour de {Name}. Choisissez une action :", actions);

            switch (choice)
            {
                case "1":
                    Attack(target);
                    break;
                case "2":
                    FrostBolt(target);
                    break;
                case "3":
                    FrostBarrier();
                    break;
                case "4":
                    ArcaneHeal();
                    break;
            }
        }
    }
}

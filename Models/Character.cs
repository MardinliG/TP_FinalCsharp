using TP_Final.Models.Enums;
using TP_Final.Models.Combat;
using TP_Final.Utils;
namespace TP_Final.Models
{
    public abstract class Character
    {
        // Propriétés de base
        public string Name { get; protected set; }
        public double CurrentHealth { get; protected set; }
        public double MaxHealth { get; protected set; }
        public double PhysicalAttackPower { get; protected set; }
        public double MagicalAttackPower { get; protected set; }
        public ArmorType ArmorType { get; protected set; }
        public double DodgeChance { get; protected set; }
        public double ParryChance { get; protected set; }
        public double SpellResistanceChance { get; protected set; }
        public bool IsDead => CurrentHealth <= 0;

        protected Character(string name, double health, double physicalAttack, double magicalAttack, 
                          ArmorType armorType, double dodgeChance, double parryChance, double spellResistance)
        {
            Name = name;
            MaxHealth = health;
            CurrentHealth = health;
            PhysicalAttackPower = physicalAttack;
            MagicalAttackPower = magicalAttack;
            ArmorType = armorType;
            DodgeChance = dodgeChance;
            ParryChance = parryChance;
            SpellResistanceChance = spellResistance;
        }

        // Méthodes de base
        public virtual void Attack(Character target)
        {
            if (IsDead || target.IsDead)
                return;

            Console.WriteLine($"{Name} attaque {target.Name}!");
            target.Defend(new Attack(this, PhysicalAttackPower, DamageType.Physical, "Attaque basique"));
        }

        public virtual void Defend(Attack attack)
        {
            if (IsDead)
                return;

            double finalDamage = CalculateDamage(attack);
            ApplyDamage(finalDamage);
        }

        public virtual void Heal(double amount)
        {
            if (IsDead)
                return;

            double actualHeal = Math.Min(MaxHealth - CurrentHealth, amount);
            CurrentHealth += actualHeal;
            Console.WriteLine($"{Name} se soigne de {actualHeal:F1} PV. PV actuels : {CurrentHealth:F1}/{MaxHealth}");
        }

        protected virtual double CalculateDamage(Attack attack)
        {
            // Gestion de l'esquive pour les attaques physiques
            if (attack.DamageType == DamageType.Physical && Random.Shared.NextDouble() < DodgeChance)
            {
                Console.WriteLine($"{Name} esquive l'attaque !");
                return 0;
            }

            // Gestion de la parade pour les attaques physiques
            if (attack.DamageType == DamageType.Physical && Random.Shared.NextDouble() < ParryChance)
            {
                Console.WriteLine($"{Name} pare partiellement l'attaque !");
                attack.Damage *= 0.5;
            }

            // Gestion de la résistance aux sorts
            if (attack.DamageType == DamageType.Magical && Random.Shared.NextDouble() < SpellResistanceChance)
            {
                Console.WriteLine($"{Name} résiste au sort !");
                return 0;
            }

            // Application de la réduction d'armure
            double damageReduction = GetArmorReduction(attack.DamageType);
            return attack.Damage * (1 - damageReduction);
        }

        protected virtual void ApplyDamage(double damage)
        {
            CurrentHealth = Math.Max(0, CurrentHealth - damage);
            Console.WriteLine($"{Name} reçoit {damage:F1} points de dégâts. PV restants : {CurrentHealth:F1}/{MaxHealth}");

            if (IsDead)
                Console.WriteLine($"{Name} est mort !");
        }

        private double GetArmorReduction(DamageType damageType)
        {
            return (ArmorType, damageType) switch
            {
                (ArmorType.Cloth, DamageType.Physical) => 0,
                (ArmorType.Cloth, DamageType.Magical) => 0.30,
                (ArmorType.Leather, DamageType.Physical) => 0.15,
                (ArmorType.Leather, DamageType.Magical) => 0.20,
                (ArmorType.Mail, DamageType.Physical) => 0.30,
                (ArmorType.Mail, DamageType.Magical) => 0.10,
                (ArmorType.Plate, DamageType.Physical) => 0.45,
                (ArmorType.Plate, DamageType.Magical) => 0,
                _ => 0
            };
        }

        public abstract void ChooseAction(Character target);
    }
}
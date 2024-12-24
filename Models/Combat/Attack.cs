using TP_Final.Models.Enums;

namespace TP_Final.Models.Combat;

public class Attack
{
    public Character Attacker { get; }
    public double Damage { get; set; }
    public DamageType DamageType { get; }
    public string Name { get; }

    public Attack(Character attacker, double damage, DamageType damageType, string name)
    {
        Attacker = attacker;
        Damage = damage;
        DamageType = damageType;
        Name = name;
    }
}
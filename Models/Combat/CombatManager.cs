namespace TP_Final.Models.Combat;

public class CombatManager
{
    private Character Player1 { get; }
    private Character Player2 { get; }

    public CombatManager(Character player1, Character player2)
    {
        Player1 = player1;
        Player2 = player2;
    }

    public void StartCombat()
    {
        while (!Player1.IsDead && !Player2.IsDead)
        {
            Player1.ChooseAction(Player2);
            if (Player2.IsDead) break;
            Player2.ChooseAction(Player1);
        }

        if (Player1.IsDead)
            Console.WriteLine($"{Player1.Name} est mort ! {Player2.Name} gagne !");
        else
            Console.WriteLine($"{Player2.Name} est mort ! {Player1.Name} gagne !");
    }
}
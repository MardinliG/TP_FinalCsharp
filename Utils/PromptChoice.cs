namespace TP_Final.Utils
{
    public static class PromptChoice
    {
        public static string GetChoice(string prompt, Dictionary<string, string> options)
        {
            Console.WriteLine(prompt);
            foreach (var option in options)
            {
                Console.WriteLine($"{option.Key}: {option.Value}");
            }

            string choice;
            do
            {
                choice = Console.ReadLine();
            } while (!options.ContainsKey(choice));

            return choice;
        }
    }
}
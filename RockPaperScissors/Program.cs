using static System.Console;
namespace RockPaperScissors
{
    partial class Program
    {
        static void Main(string[] args)
        {
            var rps = new Game("rock", "paper", "scissors", "lizard", "spock");

            int wins = 0, losses = 0, draws = 0;
            int turn = 1;
            List<string> mostUsedMoves = new List<string> { "rock", "paper", "scissors", "lizard", "spock" };

            while (true)
            {

                if (wins == 3 || losses == 3)
                    break;

                WriteLine("--------------------");
                WriteLine("LETS PLAY!");
                WriteLine("\nRound: {0}", turn);
                WriteLine("--------------------");
                WriteLine("First to win 3 rounds is the winner!");
                WriteLine("Please take your turn: " + string.Join(", ", rps.Selection) + ", or press 'q' to quit the game");
                WriteLine("");
                string selection = ReadLine().Trim().ToLower();

                if (selection == "q")
                    break;

                if (!rps.Selection.Contains(selection))

                {
                    WriteLine("Invalid choice!");
                    continue;
                }

                int result = rps.Next(selection);

                WriteLine("");
                WriteLine("You chose {0} and your opponent chose {1}!", selection, rps.LastCPUSelection);

                switch (result)

                {
                    case 1:
                        WriteLine("");
                        WriteLine("{0} beats {1} - You WON this round.", selection, rps.LastCPUSelection);
                        wins++;
                        turn++;
                        mostUsedMoves.Add(selection);
                        break;
                    case 0:

                        WriteLine("");
                        WriteLine("This round is a DRAW!");
                        draws++;
                        turn++;
                        mostUsedMoves.Add(selection);
                        break;
                    case -1:
                        WriteLine("");
                        WriteLine("{0} beats {1} - You LOST this round", rps.LastCPUSelection, selection);
                        losses++;
                        turn++;
                        mostUsedMoves.Add(selection);
                        break;
                }

                WriteLine("");
                WriteLine("Do you want to play again? Press 'y' for Yes or 'n' for No");
                if (!ReadLine().StartsWith("y", StringComparison.OrdinalIgnoreCase)) break;
            }
            WriteLine("-------------------------------------------------");
            WriteLine("MATCH RESULT - Win 3 to win the game.");
            WriteLine("-------------------------------------------------");
            turn--;
            WriteLine("The game was completed in {0} turns", turn);

            var mostUsedMove = mostUsedMoves
                .GroupBy(move => move)
                .OrderByDescending(group => group.Count())
                .First()
                .Key;
            WriteLine("The most used move was {0}", mostUsedMove);

            WriteLine("-------------------------------------------------");
            if (losses == 3 && wins < 3)
                WriteLine("You Lost the Match!");

            else if (wins == 3 && losses < 3)
                WriteLine("You Won the Match!");

            else
                WriteLine("Game Ended with no overall winner - 3 wins required");
        }
    }
}
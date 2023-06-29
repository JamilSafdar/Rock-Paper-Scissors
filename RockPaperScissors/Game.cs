namespace RockPaperScissors
{
    partial class Game
    {
        public string LastCPUSelection
        {
            private set; get;
        }

        public readonly string[] Selection;

        private CPU _cpu;
        public Game(params string[] selection)
        {
            Selection = selection;

            _cpu = new CPU(selection);
        }

        // Play next turn.
        public int Next(string selection)
        {

            string cpuSelection = _cpu.NextMove(); // Gets the CPU's next move.
            LastCPUSelection = cpuSelection; // Saves the CPU's move in a property so the player can see it.

            _cpu.AddPlayerMove(selection); // Let the CPU know the players selection
            return GetWinner(Selection, selection, cpuSelection); // Return -1 if CPU wins, 0 if draw, and 1 if player wins.
        }

        public static int GetWinner(string[] selections, string selection1, string selection2)

        {
            if (selection1 == selection2)
                return 0;

            if (GetWins(selections, selection1).Contains(selection2))
                return 1;

            else if (GetWins(selections, selection2).Contains(selection1))
                return -1;

            throw new Exception("No Winner found.");
        }

        public static IEnumerable<string> GetWins(string[] selections, string selection)
        {
            //Index of selections.
            int index = Array.IndexOf(selections, selection);

            if (index % 2 != 0 && index == selections.Length - 1)
                yield return selections[0];

            for (int i = index - 2; i >= 0; i -= 2)
                yield return selections[i];

            for (int i = index + 1; i < selections.Length; i += 2)
                yield return selections[i];
        }
    }
}
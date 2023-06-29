namespace RockPaperScissors
{
    partial class Game
    {
        class CPU
        {

            public CPU(params string[] selections)
            {
                _selections = selections;
                _selectionProbability = new Dictionary<string, int>();

                // The CPU sets the probability for each selection to be selected as 1.
                foreach (string selection in selections)
                    _selectionProbability.Add(selection, 1);

                _random = new Random();
            }

            // Probability increased of selecting each selection that beats the provided move.
            public void AddPlayerMove(string selection)
            {

                int index = Array.IndexOf(_selections, selection);

                foreach (string winningSelection in _selections.Except(GetWins(_selections, selection)))
                    if (winningSelection != selection)
                        _selectionProbability[winningSelection]++;
            }

            // Get the next move of the CPU.
            public string NextMove()
            {

                double r = _random.NextDouble();

                double divisor = _selectionProbability.Values.Sum();

                double currentPos = 0.0;

                // Maps probabilities to ranges between 0.0 and 1.0. Returns weighted random choice.

                foreach (var selection in _selectionProbability)
                {

                    double weightedRange = selection.Value / divisor;
                    if (r <= currentPos + (selection.Value / divisor))

                        return selection.Key;
                    currentPos += weightedRange;
                }

                throw new Exception("Unable to calculate move.");
            }

            Random _random;
            private readonly string[] _selections;
            private Dictionary<string, int> _selectionProbability;
        }
    }
}
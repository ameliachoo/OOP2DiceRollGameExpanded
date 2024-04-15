namespace OOP2DiceRollGameExpanded
{
    // Inherits from the base Game class
    public class ThreeOrMore : Game
    {
        // Constructor to set the game's name
        public ThreeOrMore()
        {
            Name = "ThreeOrMore";
        }

        /// <summary>
        /// Main game logic for playing the "ThreeOrMore" game.
        /// </summary>
        protected override void PlayGame()
        {
            int turn = 0; // Current player's turn
            int[] playerScores = {0, 0}; // Scores for both players
            
            // Load and update game statistics
            Statistics.LoadStatistics(this);
            GamesPlayed += 1;
            
            // Initialize an array of 5 dice
            Die[] dice = new Die[5];
            for(int i = 0; i < dice.Length; i++)
            {
                dice[i] = new Die();
            }
            
            // Game loop continues until any player's score is greater than or equal to 20
            while(playerScores.All(score => score < 20))
            {
                Console.WriteLine($"Player {turn + 1}'s turn!");
                turn = PlayerTurn(turn, playerScores, dice); // Process player's turn
            }
            
            // Determine the winner or if it's a tie
            if(playerScores[0] > playerScores[1])
            {
                Console.WriteLine("Player 1 wins!");
            }
            else if(playerScores[0] < playerScores[1])
            {
                Console.WriteLine("Player 2 wins!");
            }
            else
            {
                Console.WriteLine("It's a tie!");
            }
            
            // Save game statistics
            Statistics.SaveStatistics(this);
        }
        
        /// <summary>
        /// Counts the occurrences of each die roll.
        /// </summary>
        /// <param name="dice">The array of dice to count.</param>
        /// <returns>A dictionary containing the count of each die roll.</returns>
        private static Dictionary<int, int> CountDie(Die[] dice)
        {
            var dict = new Dictionary<int, int>();
            foreach (var t in dice)
            {
                dict.TryGetValue(t.DiceRoll, out var count);
                dict[t.DiceRoll] = count + 1;
            }
            return dict;
        }
        
        /// <summary>
        /// Simulates and runs tests for the "ThreeOrMore" game.
        /// </summary>
        /// <returns>The score of the winning player or -1 for a tie.</returns>
        public override int RunTests()
        {
            int turn = 0;
            int[] playerScores = {0, 0};
            
            // Load and update game statistics
            Statistics.LoadStatistics(this);
            GamesPlayed += 1;
            
            // Initialize dice
            Die[] dice = new Die[5];
            for(int i = 0; i < dice.Length; i++)
            {
                dice[i] = new Die();
            }
            
            // Game loop without UI interaction
            while(playerScores.All(score => score < 20))
            {
                turn = PlayerTurn(turn, playerScores, dice);
            }
            
            // Return winner's score or -1 for a tie
            if(playerScores[0] > playerScores[1])
            {
                return playerScores[0];
            }
            else if(playerScores[0] < playerScores[1])
            {
                return playerScores[0];
            }
            else
            {
                return -1;
            }
        }

        /// <summary>
        /// Executes a player's turn in the "ThreeOrMore" game.
        /// </summary>
        /// <param name="turn">The current player's turn.</param>
        /// <param name="playerScores">The scores of both players.</param>
        /// <param name="dice">The array of dice for the game.</param>
        /// <returns>The next player's turn.</returns>
        private int PlayerTurn(int turn, int[] playerScores, Die[] dice)
        {
            bool turnOver = false;

            while (!turnOver)
            {
                // Roll each die and display its value
                foreach (var t in dice)
                {
                    t.Roll();
                    Console.WriteLine($"Die {Array.IndexOf(dice, t) + 1}: {t.DiceRoll}");
                }

                // Count occurrences of each die value and display if any value occurs two or more times
                var dieValues = CountDie(dice);
                foreach (var pair in dieValues.Where(pair => pair.Value >= 2))
                {
                    Console.WriteLine($"You rolled a {pair.Key} {pair.Value} times!");
                }

                // Ask the player if they want to re-roll all or remaining dice
                Console.WriteLine("Would you like to re-roll 'all dice or the 'remaining dice? (a/r)");
                var choice = Console.ReadLine();
                if (choice?.ToLower() == "a") continue;

                Console.WriteLine("re-rolling remaining die...");

                // Re-roll dice with a count of 1
                foreach (var pair in dieValues.Where(pair => pair.Value == 1))
                {
                    dice[Array.IndexOf(dice, dice.First(d => d.DiceRoll == pair.Key))].Roll();
                }

                // Display re-rolled dice values
                foreach (var t in dice)
                {
                    Console.WriteLine($"Die {Array.IndexOf(dice, t) + 1}: {t.DiceRoll}");
                }

                // Recount die values after re-roll
                dieValues = CountDie(dice);
                foreach (var pair in dieValues)
                {
                    if (pair.Value >= 2)
                    {
                        Console.WriteLine($"You rolled a {pair.Key} {pair.Value} times!");
                    }
                }

                // Determine and display the highest frequency die value
                var max = dieValues.Aggregate((l, r) => l.Value > r.Value ? l : r).Key;
                Console.WriteLine($"The highest frequency die value is {max} with {dieValues[max]} occurrences!");

                // Update player's score based on the highest frequency die value
                switch (dieValues[max])
                {
                    case 3:
                        Console.WriteLine("\nYou got a 3-of-a-kind! +3\n");
                        playerScores[turn] += 3;
                        break;
                    case 4:
                        Console.WriteLine("\nYou got a 4-of-a-kind! +6\n");
                        playerScores[turn] += 6;
                        break;
                    case 5:
                        Console.WriteLine("\nYou got a 5-of-a-kind! +12\n");
                        playerScores[turn] += 12;
                        break;
                    default:
                        Console.WriteLine("\nYou need a 3-of-a-kind or better to gain any points! +0\n");
                        break;
                }

                // Display current turn score and check for a new high score
                Console.WriteLine($"Turn score: {playerScores[turn]}\n");
                if (playerScores[turn] > HighScore)
                {
                    HighScore = playerScores[turn];
                    Console.WriteLine("New High Score!");
                }

                // End the turn
                turnOver = true;
                turn += 1;
                turn %= 2; // Toggle between 0 and 1 to switch players
            }

            return turn; // Return the next player's turn
        }
    }
}
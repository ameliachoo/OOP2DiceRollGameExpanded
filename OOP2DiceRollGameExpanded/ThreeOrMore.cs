namespace OOP2DiceRollGameExpanded
{
    // inheriting from game class
    public class ThreeOrMore : Game
    {
        public ThreeOrMore()
        {
            Name = "ThreeOrMore";
        }

        /// <summary>
        /// main game logic for playing ThreeOrMore
        /// </summary>
        protected override void PlayGame()
        {
            bool computer = false;
            // current players turn
            int playerTurn = 0; 
            // scores for both players
            int[] twoPlayerScores = [0, 0];
            
            // load and update game statistics
            Statistics.LoadStatistics(this);
            // increments the games played
            GamesPlayed += 1;
            
            Console.WriteLine("Do you want to play against the computer? (y/n):");
            computer = Console.ReadLine() == "y";
            
            // initialize an array of five dice
            Die[] dice = new Die[5];
            // for loop creating new die objects 
            for(int i = 0; i < dice.Length; i++)
            {
                dice[i] = new Die();
            }
            
            // game loop continues until any player's score is greater than or equal to 20
            while(twoPlayerScores.All(score => score < 20))
            {
                // displays to the players what turn is next
                Console.WriteLine($"\nPlayer {playerTurn + 1}'s turn:");
                // process player's turn
                playerTurn = PlayerTurn(playerTurn, twoPlayerScores, dice, computer);
            }
            
            Console.WriteLine("\n----------");
            // determine the winner or if the game is a tie
            if(twoPlayerScores[0] > twoPlayerScores[1])
            {
                // displays which player wins
                Console.WriteLine("Player 1 wins");
            }
            else if(twoPlayerScores[0] < twoPlayerScores[1])
            {
                // displays which player wins
                Console.WriteLine("Player 2 wins");
            }
            else
            {
                // displays that the game was a tie
                Console.WriteLine("This game was a tie");
            }
            Console.WriteLine("----------");
            
            // saves game statistics
            Statistics.SaveStatistics(this);
        }
        
        /// <summary>
        /// simulates and runs tests for the ThreeOrMore game
        /// </summary>
        /// <returns>the score of the winning player or -1 for a tie</returns>
        public override int RunTests()
        {
            // current players turn
            int playerTurn = 0;
            // scores for both players
            int[] twoPlayerScores = [0, 0];
            
            // loads and updates game statistics
            Statistics.LoadStatistics(this);
            // increments the games played
            GamesPlayed += 1;
            
            // initialize an array of five dice
            Die[] dice = new Die[5];
            // for loop creating new die objects 
            for(int i = 0; i < dice.Length; i++)
            {
                dice[i] = new Die();
            }
            
            // game loop continues until any player's score is greater than or equal to 20
            while(twoPlayerScores.All(score => score < 20))
            {
                playerTurn = PlayerTurn(playerTurn, twoPlayerScores, dice, false);
            }
            
            // return winners score or -1 for a tie
            if(twoPlayerScores[0] > twoPlayerScores[1])
            {
                return twoPlayerScores[0];
            }
            else if(twoPlayerScores[0] < twoPlayerScores[1])
            {
                return twoPlayerScores[1];
            }
            else
            {
                return -1;
            }
        }
        
        /// <summary>
        /// counts the occurrences of each die roll
        /// </summary>
        /// <param name="dice">the array of dice to count</param>
        /// <returns>a dictionary containing the count of each die roll</returns>
        private static Dictionary<int, int> DieCounter(Die[] dice)
        {
            // creates a new dictionary variable to store the counts of each die roll
            var dictionary = new Dictionary<int, int>();
            
            // iterates over each Die object in the dice array
            foreach (var num in dice)
            {
                // tries to get the count of the current die roll from the dictionary
                // if the key exists then the count is retrieved otherwise it defaults to 0
                dictionary.TryGetValue(num.DiceRoll, out var count);
                // increments the count for the current die roll
                dictionary[num.DiceRoll] = count + 1;
            }
            // returns the dictionary containing the counts of each die roll
            return dictionary;
        }
        
        /// <summary>
        /// executes a players turn in the ThreeOrMore game
        /// </summary>
        /// <param name="playerTurn">the current players turn</param>
        /// <param name="twoPlayerScores">the scores of both players</param>
        /// <param name="dice">the array of dice for the game</param>
        /// <returns>the next players turn</returns>
        private int PlayerTurn(int playerTurn, int[] twoPlayerScores, Die[] dice, bool computer)
        {
            // boolean for the end of turn
            bool endOfTurn = false;
            
            while (!endOfTurn)
            {
                // roll each die and display its value
                Console.WriteLine("----------");
                foreach (var num in dice)
                {
                    num.Roll();
                    Console.WriteLine($"Die {Array.IndexOf(dice, num) + 1}: {num.DiceRoll}");
                }
                Console.WriteLine("----------");
                
                // counts occurrences of each die value and display if any value occurs two or more times
                var dieValue = DieCounter(dice);
                
                foreach (var pair in dieValue.Where(pair => pair.Value >= 2))
                {
                    // displays to the player what they have rolled multiple times
                    Console.WriteLine($"You rolled a {pair.Key}, {pair.Value} times.");
                }

                if (playerTurn == 1 && computer == false)
                {
                    // ask the player if they want to re-roll all or remaining dice
                    Console.WriteLine(
                        "\nIf you would like to re-roll all dice, type 'a'.\nIf you would like to re-roll the remaining dice, type 'r'.");
                    // reads what the player inputs
                    var rollChoice = Console.ReadLine();

                    // if the roll choice is an a then the program continues
                    if (rollChoice?.ToLower() == "a")
                    {
                        continue;
                    }
                }
                else if(playerTurn == 0) // multiplayer off
                {
                    // ask the player if they want to re-roll all or remaining dice
                    Console.WriteLine(
                        "\nIf you would like to re-roll all dice, type 'a'.\nIf you would like to re-roll the remaining dice, type 'r'.");
                    // reads what the player inputs
                    var rollChoice = Console.ReadLine();

                    // if the roll choice is an a then the program continues
                    if (rollChoice?.ToLower() == "a")
                    {
                        continue;
                    }
                }

                
                // lets the player know the remaining die are being rerolled otherwise
                Console.WriteLine("\nNow re-rolling remaining die.");

                // re-roll dice with a count of 1
                foreach (var pair in dieValue.Where(pair => pair.Value == 1))
                {
                    dice[Array.IndexOf(dice, dice.First(d => d.DiceRoll == pair.Key))].Roll();
                }

                // displays the re-rolled dice values
                Console.WriteLine("----------");
                foreach (var num in dice)
                {
                    Console.WriteLine($"Die {Array.IndexOf(dice, num) + 1}: {num.DiceRoll}");
                }
                Console.WriteLine("----------");
                
                dieValue = DieCounter(dice);
                // recount the die values after the re-roll
                foreach (var pair in dieValue)
                {
                    if (pair.Value >= 2)
                    {
                        // displays to the player what they have rolled multiple times
                        Console.WriteLine($"You rolled a {pair.Key}, {pair.Value} times.");
                    }
                }
                

                // determines and display the highest frequency die value
                var frequentRoll = dieValue.MaxBy(kv => kv.Value).Key;
                // outputs this result to the players
                Console.WriteLine($"\nThe most frequently rolled die value is {frequentRoll}, being rolled {dieValue[frequentRoll]} times.");

                // update players score based on the highest frequency die value
                switch (dieValue[frequentRoll])
                {
                    case 3:
                        Console.WriteLine("You got a three of a kind. +3 points.");
                        twoPlayerScores[playerTurn] += 3;
                        break;
                    case 4:
                        Console.WriteLine("You got a four of a kind +6 points.");
                        twoPlayerScores[playerTurn] += 6;
                        break;
                    case 5:
                        Console.WriteLine("You got a five of a kind, +12 points.");
                        twoPlayerScores[playerTurn] += 12;
                        break;
                    default:
                        Console.WriteLine("You must get a three of a kind or more to earn points, no points earned.");
                        break;
                }

                // display current turn score to the player
                Console.WriteLine($"\nYour score this turn was: {twoPlayerScores[playerTurn]}");
                // changes the player score to be the high score if a high score is achieved
                if (twoPlayerScores[playerTurn] > HighScore)
                {
                    HighScore = twoPlayerScores[playerTurn];
                    Console.WriteLine("You got a new high score.");
                }

                // ends the players turn
                endOfTurn = true;
                // increments the turn
                playerTurn += 1;
                // uses MOD to swap between two values (switching between player 1(0) and player 2(1)
                playerTurn %= 2;
            }
            // returns the next players turn
            return playerTurn;
        }
    }
}
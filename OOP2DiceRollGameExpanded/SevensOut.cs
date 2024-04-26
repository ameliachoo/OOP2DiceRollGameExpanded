namespace OOP2DiceRollGameExpanded;

// inheriting from the game class
public class SevensOut : Game {
    
    /// <summary>
    /// constructor for the SevensOut class
    /// sets the name of the game to 'SevensOut'
    /// </summary>
    public SevensOut() {
        Name = "SevensOut";
    }
    
    /// <summary>
    /// overrides the base class method to play the SevensOut game
    /// allows the player to roll two dice repeatedly until the sum of the rolls is 7
    /// the player's total score and high score are tracked and displayed
    /// after each game, the statistics are saved using the SaveStatistics method
    /// </summary>
    protected override void PlayGame() {
        // total score for the player (starts at 0)
        int scoreTotal = 0;
        int x = 0;
        // flag to indicate whether the game is over (starts as false)
        bool gameOver = false;
        // incrementing games played
        GamesPlayed += 1;
        // loading the statistics method
        Statistics.LoadStatistics(this);
        
        // displays to the player what game they have chosen to play
        Console.WriteLine("\n--------\nYou are now playing Sevens Out\n");
        
        // creates an array of two dice objects 
        Die[] dice = new Die[2];
        // initialises each Die object in the array
        for (int i = 0; i < dice.Length; i++) {
            dice[i] = new Die();
        }

        while (!gameOver) {
            // display the current total score and prompt the user to roll the dice again
            Console.WriteLine($"\nYour current total: {scoreTotal} \nPress enter to roll the dice again");
            Console.ReadLine();
            //increment the roll count
            x++;

            // rolls each die in the dice array
            foreach (var num in dice) {
                num.Roll();
            }
            
            // displays the roll number
            Console.WriteLine($"\nRoll {x}:");
            // displays the value of the first and second die 
            Console.WriteLine($"\nDie 1: {dice[0].DiceRoll} \nDie 2: {dice[1].DiceRoll}");
            
            gameOver = (dice[0].DiceRoll + dice[1].DiceRoll == 7);
            
            // checking if the values of both dice are the same
            if (dice[0].DiceRoll == dice[1].DiceRoll) {
                // if the values are the same...
                // the sum is doubled...
                // and added to the total score
                scoreTotal += (dice[0].DiceRoll + dice[1].DiceRoll) * 2;
            }
            else {
                // if the values are different...
                // adds the sum of the dice values to the total score
                scoreTotal += dice[0].DiceRoll + dice[1].DiceRoll;
            }
        }
        
        // displays the game over message
        Console.WriteLine("\nGame Over!\n--------");
        // displays the final total score
        Console.WriteLine($"\nYou rolled a total of seven, with your end total being: {scoreTotal}");
        
        // checks if the final score is higher than the current high score
        if (scoreTotal > HighScore) {
            // sets the new score to be the high score and outputs a message to the user 
            HighScore = scoreTotal;
            Console.WriteLine("\nYou got a new high score!\n--------");
        }
        // saving the current statistics using the SaveStatistics method
        Statistics.SaveStatistics(this);
    }

    /// <summary>
    /// overrides the base class method to run tests for the SevensOut game
    /// simulates playing the game multiple times
    /// tracks the total score achieved and the number of rolls taken to get a sum of 7
    /// the test results are returned as an integer representing the sum of the dice rolls in the last test
    /// </summary>
    /// <returns>
    /// integer representing the sum of the dice rolls in the last test
    /// </returns>
    public override int RunTests() {
        int scoreTotal = 0;
        int x = 0;
        // flag to indicate whether the game is over (starts as false)
        bool gameOver = false;
        // test results (set to 0)
        int testResults = 0;
 
        while (!gameOver) {
            // creating an array of two dice objects
            Die[] dice = new Die[2];
            // initialising each die object in the array
            for (int i = 0; i < dice.Length; i++) {
                dice[i] = new Die();
            }
            
            //increment the test count
            x++;
            // rolling each die in the array
            foreach (var num in dice) {
                num.Roll();
            }
            
            // calculating the sum of the dice rolls
            testResults = dice[0].DiceRoll + dice[1].DiceRoll;
            // checking if the game is over (sum of dice is 7)
            gameOver = (dice[0].DiceRoll + dice[1].DiceRoll == 7);
            
            // checking if the dice rolls are the same
            if (dice[0].DiceRoll == dice[1].DiceRoll) {
                // if the values are the same...
                // the sum is doubled...
                // and added to the total score
                scoreTotal += (dice[0].DiceRoll + dice[1].DiceRoll) * 2;
            }
            else {
                // if the values are different...
                // adds the sum of the dice values to the total score
                scoreTotal += dice[0].DiceRoll + dice[1].DiceRoll;
            }
        }
        // returning the test results
        return testResults;
    }
}
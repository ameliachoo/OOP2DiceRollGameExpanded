namespace OOP2DiceRollGameExpanded;

public abstract class Game {
    // 'protected': this access modifier restricts the visibility of the method to within its own class and any subclasses
    // 'abstract': this keyword indicates that the method does not have a body or implementation in the current class
    protected abstract void PlayGame();
    public abstract int RunTests();
    
    public string Name { get; set; } = "";
    public int GamesPlayed { get; set; } = 0;
    public int HighScore { get; set; } = 0;
    
    /// <summary>
    /// main function where the program execution starts
    /// </summary>
    /// <param name="args"></param>
    static void Main(string[] args) {
        // array to hold game options
        Game[] gameOptions =
        [
            new SevensOut(),
            new ThreeOrMore()
        ];
        
        // prompt the user to type 'Test' or 'Reset'
        Console.WriteLine("\nType 'test' to run tests on the program, 'stats' to display statistics and 'reset' to reset the statistics.\n");
        // print the available games and the number they correlate to 
        Console.WriteLine("Games:\n----------------\nSevensOut = 1\nThreeOrMore = 2\n----------------");
        // calls the function to handle game selection
        GameSelection(gameOptions);
    }

    /// <summary>
    /// method to handle game selection, user input, and game execution
    /// continuously prompts the user to choose a game or enter commands (reset / test)...
    /// ...and validates the input before executing the selected game or command
    /// </summary>
    /// <param name="gameOptions">Array of available game options.</param>
    private static void GameSelection(Game[] gameOptions) {

        while (true) {
            // sets the choice variable to 0
            int choice = 0;
            // asks for user input (which game-mode they wish to play)
            Console.WriteLine("Please choose a game (1 / 2):");
            
            // sets the gameChoice to be whatever the user has inputted
            var gameChoice = Console.ReadLine();
            
            // if the game choice is null
            // the program attempts to parse the users input to an integer and subtracts one
            // to get to the correct index
            // lets the user input a choice that is not a number
            if (gameChoice != null && !(gameChoice?.ToLower() == "test" || gameChoice?.ToLower() == "reset" || gameChoice?.ToLower() == "stats")) {
                choice = int.Parse(gameChoice) - 1;
                // calls the play game method of the selected game option
                gameOptions[choice].PlayGame();
                break;
            }
            
            // switch case for whether the user wishes to test or reset (currently empty)
            switch (gameChoice) {
                // if test is entered, return from the method
                case "test":
                    Testing.RunTest();
                    break;
                // displays the statistics
                case "stats":
                    Statistics.DisplayStatistics();
                    return;
                // if reset is entered, return from the method
                case "reset":
                    Statistics.ResetStatistics();
                    Console.WriteLine("Statistics have been reset.");
                    break;
                // continue loop for any other inputs
                default:
                    break;
            }
        }        
    }
}
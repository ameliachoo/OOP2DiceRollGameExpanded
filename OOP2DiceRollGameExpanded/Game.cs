using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2DiceRollGameExpanded;

public abstract class Game
{
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
    static void Main(string[] args)
    {
        // array to hold game options
        Game[] gameOptions =
        [
            new SevensOut(),
            new ThreeOrMore()
        ];
        
        // print the available games and the number they correlate to 
        Console.WriteLine("Games:\nSevensOut = 1\nThreeOrMore = 2\n-----------");
        // prompt the user to type 'Test' or 'Reset'
        Console.WriteLine("\nType 'Test' to run tests on the program and 'Reset' to reset the statistics");
        // calls the function to handle game selection
        GameSelection(gameOptions);
    }

    /// <summary>
    /// method to handle game selection, user input, and game execution
    /// continuously prompts the user to choose a game or enter commands (reset / test)...
    /// ...and validates the input before executing the selected game or command
    /// </summary>
    /// <param name="gameOptions">Array of available game options.</param>
    private static void GameSelection(Game[] gameOptions)
    {

        while (true)
        {
            // sets the choice variable to 0
            int choice = 0;
            // asks for user input (which game-mode they wish to play)
            Console.WriteLine("\nPlease choose a game (Sevens Out or Three Or More):");
            
            // sets the gameChoice to be whatever the user has inputted
            var gameChoice = Console.ReadLine();
            
            // try contains the code that might throw and exception
            try
            {
                // if the game choice is null
                // the program attempts to parse the users input to an integer and subtracts one
                // to get to the correct index
                if (gameChoice != null)
                {
                    choice = int.Parse(gameChoice) - 1;
                }
                
                // calls the play game method of the selected game option
                gameOptions[choice].PlayGame();
                break;
            }
            // this handles any exceptions that are thrown in the try block
            catch (Exception e)
            {
                // if an exception occurs (e.g., invalid input or index out of range) 
                // the program jumps to this catch block instead of crashing
                Console.WriteLine("The game number you have entered was not found");
            }
            
            // switch case for whether the user wishes to test or reset (currently empty)
            switch (gameChoice)
            {
                // if test is entered, return from the method
                case "Test":
                    return;
                
                // if reset is entered, return from the method
                case "Reset":
                    return;
                
                // continue loop for any other inputs
                default:
                    break;
            }
        }        
    }

}
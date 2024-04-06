using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2DiceRollGameExpanded;

public class SevensOut : Game
{
    /// <summary>
    /// constructor for the SevensOut class
    /// sets the name of the game to 'SevensOut'
    /// </summary>
    public SevensOut()
    {
        Name = "SevensOut";
    }
    
    /// <summary>
    /// method to play the SevensOut game (override)
    /// </summary>
    protected override void PlayGame()
    {
        // flag to indicate whether the game is over (starts as false)
        bool gameOver = false;
        // total score for the player (starts at 0)
        int scoreTotal = 0;
        
        int x = 0;

        GamesPlayed += 1;
        
        // displays to the player what game they have chosen to play
        Console.WriteLine("\n--------\nYou are now playing Sevens Out\n");

        // creates an array of two dice objects 
        Die[] dice = new Die[2];
        // initialises each Die object in the array
        for (int i = 0; i < dice.Length; i++)
        {
            dice[i] = new Die();
        }

        while (!gameOver)
        {
            // display the current total score and prompt the user to roll the dice again
            Console.WriteLine($"\nYour current total: {scoreTotal} \nPress space to roll the dice again");
            
            //increment the roll count
            x++;

            // rolls each die in the dice array
            foreach (var num in dice)
            {
                num.Roll();
            }
            
            // displays the roll number
            Console.WriteLine($"\nRoll {x}:");
            // displays the value of the first and second die 
            Console.WriteLine($"\nDie 1: {dice[0].DiceRoll} \nDie 2: {dice[1].DiceRoll}");
            
            // checking if the values of both dice are the same
            if (dice[0].DiceRoll == dice[1].DiceRoll)
            {
                // if the values are the same...
                // the sum is doubled...
                // and added to the total score
                scoreTotal += (dice[0].DiceRoll + dice[1].DiceRoll) * 2;
            }
            else
            {
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
        if (scoreTotal > HighScore)
        {
            // sets the new score to be the high score and outputs a message to the user 
            HighScore = scoreTotal;
            Console.WriteLine("\nYou got a new high score!\n--------");
        }
    }
    
    
    
    
    public override int RunTests()
    {
        // flag to indicate whether the game is over (starts as false)
        bool gameOver = false;
        // total score for the player (starts at 0)
        int scoreTotal = 0;
        
        int x = 0;

        int testResults = 0;
        
        return 0;
    }
    
}
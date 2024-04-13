using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2DiceRollGameExpanded;

public static class Testing
{
    /// <summary>
    /// method to run tests for the dice rolling game
    /// </summary>
    public static void RunTest() 
    {
        // display messages indicating that tests are running
        Console.WriteLine("-----------\nRunning appropriate tests");

        // create a new Die object for testing
        var die = new Die();
        
        
        // loops 1000 times to run multiple tests
        for (int i = 0; i < 1000; i++)
        {
            // rolls the die
            die.Roll();
            // assert that the rolled value is between 1 and 6 (inclusive)
            Debug.Assert(die.DiceRoll is >= 1 and <= 6);
        }
        
        // array to hold game options
        Game[] gameOptions =
        {
            new SevensOut(),
            new ThreeOrMore()
        };
        
        // runs tests for SevensOut game and store the results
        int resultSevens = gameOptions[0].RunTests();
        Debug.Assert(resultSevens == 7, $"Result:{resultSevens}");
        // runs tests for ThreeOrMore game and store the results
        int resultThree = gameOptions[1].RunTests();
        Debug.Assert(resultThree < 20 && resultThree != -1, $"Result:{resultThree}");
 
        // prints message indicating all tests passed
        Debug.WriteLine("All tests passed.");
        
        string logPath = "../../../tests.log";
        using (StreamWriter sw = new StreamWriter(logPath))
        {
            sw.WriteLine($"SevensOut test result: {resultSevens}");
            sw.WriteLine($"ThreeOrMore test result: {resultThree}");
            
        }
        
        Console.WriteLine("Tests complete");
    }
}

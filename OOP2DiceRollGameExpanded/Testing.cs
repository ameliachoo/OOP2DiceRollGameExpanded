using System.Diagnostics;

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
        
        // list for dice rolls
        List<int> rolls = [];
        // create a new Die object for testing
        var die = new Die();
        
        // loops 1000 times to run multiple tests
        for (int i = 0; i < 1000; i++)
        {
            // rolls the die
            die.Roll();
            // adds the dice roll values to a list
            rolls.Add(die.DiceRoll);
            // assert that the rolled value is between 1 and 6 (inclusive)
            Debug.Assert(die.DiceRoll is >= 1 and <= 6, $"Dice value = {die.DiceRoll}\n The die value is out of range.");
            // assert that the number of rolls isn't impossible
            Debug.Assert(rolls.Count == 1000, "Dice roll count seems to be incorrect");
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
        
        // define the path for the log file
        string logTestsFile = "../../../tests.log";
        // create a StreamWriter object to write to the log file
        using (StreamWriter sw = new StreamWriter(logTestsFile))
        {
            // write the rolls list to the log file, converting it to a comma-separated string
            sw.WriteLine(string.Join(",", rolls));
            // write the number of rolls to the log file
            sw.WriteLine($"Number of rolls: {rolls.Count}");
            
            // write the result of the SevensOut test to the log file
            sw.WriteLine($"SevensOut test result: {resultSevens}");
            // w the result of the ThreeOrMore test to the log file
            sw.WriteLine($"ThreeOrMore test result: {resultThree}");
            
        }
        // print message indicating all tests have been completed and there results
        Console.WriteLine("All tests have been completed");
        Console.WriteLine($"\nTests are displayed in the log file: {logTestsFile}");
    }
}

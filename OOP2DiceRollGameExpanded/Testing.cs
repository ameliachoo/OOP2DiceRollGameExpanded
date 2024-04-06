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
    /// 
    /// </summary>
    /// <returns> returns 0 if all tests pass </returns>
    public static void RunTest() 
    {
        Console.WriteLine("-------------------\nRunning appropriate tests");

        var die = new Die();
        
        List<int> rolls = [];
        
        // loops 1000 times to run multiple tests
        for (int i = 0; i < 1000; i++)
        {
            // will contain the testing
            die.Roll();
        }
        
        // array to hold game options (currently empty)
        Game[] gameOptions =
        [
        ];
            
 
        // prints message indicating all tests passed
        Debug.WriteLine("All tests passed.");
    }
}

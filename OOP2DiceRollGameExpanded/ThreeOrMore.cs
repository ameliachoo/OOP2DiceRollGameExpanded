using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP2DiceRollGameExpanded;

public class ThreeOrMore : Game
{
    public ThreeOrMore()
    {
        Name = "ThreeOrMore";
    }

    protected override void PlayGame()
    {
        Console.WriteLine("Hello World");
    }
    public override int RunTests()
    {
        return 0;
    } 
}
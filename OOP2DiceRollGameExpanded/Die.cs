namespace OOP2DiceRollGameExpanded;

public class Die
{
    // static random number generator for all instances of Die
    private static Random _rnd = new Random();
        
    /// <summary>
    /// represents the current value of the die roll
    /// </summary>
    public int DiceRoll = _rnd.Next(1, 7);
        
    /// <summary>
    /// rolls the die and returns the result
    /// </summary>
    /// <returns> returns the DiceRoll </returns>
    public int Roll() {
            
        // generates a random number between 1 and 6 (inclusive) to simulate a die roll
        DiceRoll = _rnd.Next(1, 7);
            
        // returns the result of the die roll
        return DiceRoll;
    }
}
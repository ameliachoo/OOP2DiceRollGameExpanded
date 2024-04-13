using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace OOP2DiceRollGameExpanded;

public class Statistics
{
    private const string FilePath = "../../../stats.csv";

    public static void LoadStatistics(Game game)
    {
        List<string> lines;
        
        try
        {
            lines = File.ReadAllLines(FilePath).ToList();
        }
        catch (FileNotFoundException)
        {
            ResetStatistics();
            LoadStatistics(game);
            return;
        }

        var existingLine = lines.FirstOrDefault(line => line.StartsWith(game.Name));

        if (existingLine != null)
        {
            var parts = existingLine.Split(',');
            game.GamesPlayed = int.Parse(parts[1]);
            game.HighScore = int.Parse(parts[2]);
        }
    }

    public static void SaveStatistics(Game game)
    {
        List<string> lines;

        try
        {
            lines = File.ReadAllLines(FilePath).ToList();
        }
        catch (FileNotFoundException)
        {
            ResetStatistics();
            SaveStatistics(game);
            return;
        }

        var existingLine = lines.FirstOrDefault(line => line.StartsWith(game.Name));

        if (existingLine != null)
        {
            var parts = existingLine.Split(',');
            parts[1] = game.GamesPlayed.ToString();
            parts[2] = game.HighScore.ToString();
            lines[lines.IndexOf(existingLine)] = string.Join(',', parts);
        }
        else
        {
            lines.Add($"{game.Name},{game.GamesPlayed},{game.HighScore}");
        }

        File.WriteAllLines(FilePath, lines);
    }

    public static void ResetStatistics()
    {
        var lines = new List<string>
        {
            "Sevens Out,0,0",
            "Three Or More,0,0"
        };

        File.WriteAllLines(FilePath, lines);
    }
}
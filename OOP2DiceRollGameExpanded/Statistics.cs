using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CsvHelper;
using CsvHelper.Configuration;

namespace OOP2DiceRollGameExpanded;

public class Statistics
{
    private const string FilePath = "../../../stats.csv";
    
    public static void LoadStatistics(Game game)
    {
        List<StatsRecord> records;
        try
        {
            using var reader = new StreamReader(FilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            records = csv.GetRecords<StatsRecord>().ToList();
        }
        catch (FileNotFoundException)
        {
            ResetStatistics();
            LoadStatistics(game);
            return;
        }

        var existingRecord = records.FirstOrDefault(r => r.GameName == game.Name);

        if (existingRecord != null)
        {
            game.TimesPlayed = existingRecord.TotalPlays;
            game.HighScore = existingRecord.HighScore;
        }
    }

    public static void SaveStatistics(Game game)
    {
        List<StatsRecord> records;
        try
        {
            using var reader = new StreamReader(FilePath);
            using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture));
            records = csv.GetRecords<StatsRecord>().ToList();
        }
        catch (FileNotFoundException)
        {
            ResetStatistics();
            SaveStatistics(game);
            return;
        }

        var existingRecord = records.FirstOrDefault(r => r.GameName == game.Name);
        
        if (existingRecord != null)
        {
            existingRecord.TotalPlays = game.TimesPlayed;
            existingRecord.HighScore = game.HighScore;
        }
        else
        {
            records.Add(new StatsRecord
            {
                GameName = game.Name,
                TotalPlays = game.TimesPlayed,
                HighScore = game.HighScore
            });
        }

        using var writer = new StreamWriter(FilePath);
        using var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
        csvWriter.WriteRecords(records);
    }

    public static void ResetStatistics()
    {
        var records = new List<StatsRecord>
        {
            new StatsRecord { GameName = "Sevens Out", TotalPlays = 0, HighScore = 0 },
            new StatsRecord { GameName = "Three Or More", TotalPlays = 0, HighScore = 0 }
        };

        using var writer = new StreamWriter(FilePath);
        using var csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture));
        csvWriter.WriteRecords(records);
    }

    private class StatsRecord
    {
        public string GameName { get; set; }
        public int TotalPlays { get; set; }
        public int HighScore { get; set; }

    }
}
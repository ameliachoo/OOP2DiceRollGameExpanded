namespace OOP2DiceRollGameExpanded
{
    public class Statistics
    {
        // file path where the statistics are stored as a CSV file
        private static string _filePath = @"../../../stats.csv";

        /// <summary>
        /// loads the statistics for a specific game from the CSV file
        /// if the file does not exist it resets the statistics and tries again
        /// </summary>
        /// <param name="game">the game object to load the statistics into</param>
        public static void LoadStatistics(Game game)
        {
            List<string> lines;

            // attempt to read all lines from the CSV file
            try
            {
                lines = File.ReadAllLines(_filePath).ToList();
            }
            catch (FileNotFoundException)
            {
                // if the file is not found it resets the statistics and try again
                ResetStatistics();
                LoadStatistics(game);
                return;
            }

            // finds the line corresponding to the game in the CSV file
            var existingLine = lines.FirstOrDefault(line => line.StartsWith(game.Name));

            if (existingLine != null)
            {
                // if the line exists it parses the values and set them to the game object
                var parts = existingLine.Split(',');
                game.GamesPlayed = int.Parse(parts[1]);
                game.HighScore = int.Parse(parts[2]);
            }
        }

        /// <summary>
        /// saves the statistics for a specific game to the CSV file
        /// if the file does not exist it resets the statistics and tries again
        /// </summary>
        /// <param name="game">the game object containing the statistics to save</param>
        public static void SaveStatistics(Game game)
        {
            List<string> lines;

            // attempts to read all lines from the CSV file
            try
            {
                lines = File.ReadAllLines(_filePath).ToList();
            }
            catch (FileNotFoundException)
            {
                // if the file is not found it resets the statistics and try again
                ResetStatistics();
                SaveStatistics(game);
                return;
            }

            // find the line corresponding to the game in the CSV file
            var existingLine = lines.FirstOrDefault(line => line.StartsWith(game.Name));

            if (existingLine != null)
            {
                // if the line exists it updates the values and save back to the CSV file
                var parts = existingLine.Split(',');
                parts[1] = game.GamesPlayed.ToString();
                parts[2] = game.HighScore.ToString();
                lines[lines.IndexOf(existingLine)] = string.Join(',', parts);
            }
            else
            {
                // if the line does not exist it adds a new line with the games statistics
                lines.Add($"{game.Name},{game.GamesPlayed},{game.HighScore}");
            }

            // write all lines back to the CSV file
            File.WriteAllLines(_filePath, lines);
        }

        /// <summary>
        /// resets the statistics for all games and writes them to the CSV file
        /// </summary>
        public static void ResetStatistics()
        {
            if (!File.Exists(_filePath)) File.Create(_filePath);
            
            // initialize the statistics for the games
            var lines = new List<string>
            {
                "Sevens Out,0,0",
                "Three Or More,0,0"
            };

            // write all lines to the CSV file
            File.WriteAllLines(_filePath, lines);
        }
    }
}
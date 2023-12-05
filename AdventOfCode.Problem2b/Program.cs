using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Problem2a
{
    class Program
    {
        static void Main(string[] args)
        {
            Regex regex = new Regex($"Game [0-9]*:");
            Regex cubes = new Regex($"[0-9]* blue|[0-9]* red|[0-9]* green");
            int totalRed = 12;
            int totalGreen = 13;
            int totalBlue = 14;

            int gameIdTotal = 0;
            string fileName = string.Empty;

            fileName = @"C:\Source Code\Problem 2 Input.txt";


            if (fileName.Equals(string.Empty))
            {
                if (args.Length <= 0)
                {
                    Console.Out.WriteLine("Please provide a file path for a file to be analyzed:");
                    fileName = Console.ReadLine();
                }
                else
                    fileName = args[0];
            }

            FileStream fs = new FileStream(fileName, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            do
            {
                string gameId = string.Empty;

                string currentLine = sr.ReadLine();

                Console.Out.WriteLine(currentLine);
                MatchCollection matches = regex.Matches(currentLine);

                foreach (Match match in matches)
                {
                    int spaceIndex = currentLine.IndexOf(" ") + 1;
                    int colonIndex = currentLine.IndexOf(":");

                    int length = colonIndex - spaceIndex == 0 ? 1 : colonIndex - spaceIndex;
                    gameId = currentLine.Substring(spaceIndex, length);
                }

                string[] gameTurns = currentLine.Split(';');
                bool isGameValid = true;

                //Current game highest recorded
                int highestRed = 0;
                int highestBlue = 0;
                int highestGreen = 0;

                foreach (string gameTurn in gameTurns)
                {
                    Console.Out.WriteLine(gameTurn);

                    int currentTurnRed = 0;
                    int currentTurnBlue = 0;
                    int currentTurnGreen = 0;

                    matches = cubes.Matches(gameTurn);
                    foreach (Match match in matches)
                    {
                        string matchValue = match.Value;
                        string cubeCount = matchValue.Substring(0, matchValue.IndexOf(" "));
                        if (matchValue.Contains("red"))
                        {
                            currentTurnRed = int.Parse(cubeCount);
                            if (currentTurnRed > highestRed)
                                highestRed = currentTurnRed;
                        }
                        if (matchValue.Contains("green"))
                        {
                            currentTurnGreen = int.Parse(cubeCount);
                            if (currentTurnGreen > highestGreen)
                                highestGreen = currentTurnGreen;
                        }
                        if (matchValue.Contains("blue"))
                        {
                            currentTurnBlue = int.Parse(cubeCount);
                            if (currentTurnBlue > highestBlue)
                                highestBlue = currentTurnBlue;
                        }
                    }
                }

                int gamePower = highestRed * highestBlue * highestGreen;
                Console.Out.WriteLine($"Minimum cube count for game {gameId}:{Environment.NewLine}red: {highestRed} green: {highestGreen} blue: {highestBlue}");
                Console.Out.WriteLine($"Game power: { gamePower}");
                gameIdTotal += gamePower;                

            } while (!sr.EndOfStream);

            Console.Out.WriteLine($"Total of IDs for all possible games: {gameIdTotal}");
            Console.ReadKey();
        }
    }
}

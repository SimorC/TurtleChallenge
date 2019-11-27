using System;
using System.Collections.Generic;
using System.Linq;
using TurtleChallenge.ConsoleApp.Helper;
using TurtleChallenge.Domain.Interfaces;
using TurtleChallenge.Domain.Model;
using TurtleChallenge.Domain.Model.Enum;
using TurtleChallenge.Domain.Model.Extension;

namespace TurtleChallenge.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                IFileData fileData = NinjectHelper.GetFileData();

                SetConfig(fileData);
                SetSequences(fileData);

                List<GameOver> lstGameOver = Game.GameBoard.ExecuteSequences(Game.Sequences).ToList();

                WriteOutput(lstGameOver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void WriteOutput(List<GameOver> lstGameOver)
        {
            for (int i = 0; i < lstGameOver.Count; i++)
            {
                Console.WriteLine($"Sequence {i + 1}: {lstGameOver[i].ToFriendlyString()}");
            }
        }

        private static void SetSequences(IFileData fileData)
        {
            bool flagOk = false;

            do
            {
                Console.WriteLine("Please enter the path for the sequences file:");
                //string configPath = @"C:\Users\Simor\source\repos\TurtleChallenge\TurtleChallenge\TurtleChallenge.Test\StepFiles\FinishMultipleSequence.json";
                string configPath = Console.ReadLine();

                flagOk = TrySetSteps(fileData, flagOk, configPath);
            } while (!flagOk);
        }

        private static bool TrySetSteps(IFileData fileData, bool flagOk, string stepsPath)
        {
            try
            {
                fileData.LoadSequencesFile(stepsPath).Wait();
                flagOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return flagOk;
        }

        private static void SetConfig(IFileData fileData)
        {
            bool flagOk = false;

            do
            {
                Console.WriteLine("Please enter the path for the configuration file:");
                //string configPath = @"C:\Users\Simor\source\repos\TurtleChallenge\TurtleChallenge\TurtleChallenge.Test\ConfigurationFiles\CorrectConfig.json";
                string configPath = Console.ReadLine();

                flagOk = TrySetConfig(fileData, flagOk, configPath);
            } while (!flagOk);
        }

        private static bool TrySetConfig(IFileData fileData, bool flagOk, string configPath)
        {
            try
            {
                Game.GameBoard = fileData.LoadConfigurationFile(configPath, Game.GameBoard).GetAwaiter().GetResult();
                flagOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return flagOk;
        }
    }
}

﻿using System;
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
                IGameService gameService = NinjectHelper.GetGameService();

                SetConfig(gameService);
                SetSequences(gameService);

                List<GameOver> lstGameOver = Game.GameBoard.ExecuteSequences(Game.Sequences).ToList();

                WriteOutput(lstGameOver);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            Console.ReadKey();
        }

        private static void SetConfig(IGameService gameService)
        {
            bool flagOk = false;

            do
            {
                Console.WriteLine("Please enter the path for the configuration file (path + file name):");
                string configPath = Console.ReadLine();

                flagOk = TrySetConfig(gameService, flagOk, configPath);
            } while (!flagOk);
        }

        private static bool TrySetConfig(IGameService gameService, bool flagOk, string configPath)
        {
            try
            {
                Game.GameBoard = gameService.GetBoardFromConfigurationFile(configPath);
                flagOk = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }

            return flagOk;
        }

        private static void WriteOutput(List<GameOver> lstGameOver)
        {
            for (int i = 0; i < lstGameOver.Count; i++)
            {
                Console.WriteLine($"Sequence {i + 1}: {lstGameOver[i].ToFriendlyString()}");
            }
        }

        private static void SetSequences(IGameService gameService)
        {
            bool flagOk = false;

            do
            {
                Console.WriteLine("Please enter the path for the sequences file (path + file name):");
                string configPath = Console.ReadLine();

                flagOk = TrySetSteps(gameService, flagOk, configPath);
            } while (!flagOk);
        }

        private static bool TrySetSteps(IGameService gameService, bool flagOk, string stepsPath)
        {
            try
            {
                Game.Sequences = gameService.GetSequencesFromFile(stepsPath);
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

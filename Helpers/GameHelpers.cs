using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rock_Paper_Scissors.Models;
using Rock_Paper_Scissors.ViewModels;
using static Rock_Paper_Scissors.Enums.Enums;

namespace Rock_Paper_Scissors.Helpers
{
    public static class GameHelpers
    {
        public static void BeginnerMove(int beginnerCount, GameViewModel gameViewModel, Enum_Choice[] choices, bool isPlayerOne, ControllerBase controllerBase)
        {
            //CPU has set pattern of between 3 and 5 choices. CPU has a 1 in 20 chance of refreshing pattern.
            Random random = new Random();
            int randomNumUpTo19 = random.Next(20);

            if (controllerBase.HttpContext.Session.GetString("BeginnerCpuPattern") == null || randomNumUpTo19 == 19)
            {
                List<Enum_Choice> beginnerChoicePattern = new List<Enum_Choice>();

                int patternCount = random.Next(3, 6);

                for (int i = 0; i < patternCount; i++)
                {
                    int numUpTo2 = random.Next(3);
                    Enum_Choice randomChoice = choices[numUpTo2];
                    beginnerChoicePattern.Add(randomChoice);
                }

                beginnerCount = 0;

                controllerBase.HttpContext.Session.SetString("BeginnerCpuPattern", JsonConvert.SerializeObject(beginnerChoicePattern));
            }

            var cpuChoices = JsonConvert.DeserializeObject<List<Enum_Choice>>(controllerBase.HttpContext.Session.GetString("BeginnerCpuPattern"));

            Enum_Choice cpuChoice = cpuChoices[beginnerCount % cpuChoices.Count];

            if (isPlayerOne)
            {
                gameViewModel.Game_PlayerChoice1 = cpuChoice;
                gameViewModel.Game_BeginnerCpuMoveCount1 = beginnerCount + 1;
            }
            else
            {
                gameViewModel.Game_PlayerChoice2 = cpuChoice;
                gameViewModel.Game_BeginnerCpuMoveCount2 = beginnerCount + 1;
            }
        }

        public static void IntermediateMove(GameViewModel gameViewModel, Enum_Choice[] choices, bool isPlayerOne)
        {
            Random random = new Random();
            int randomNumber = random.Next(3);

            Enum_Choice cpuChoice = choices[randomNumber];

            if (isPlayerOne)
                gameViewModel.Game_PlayerChoice1 = cpuChoice;
            else
                gameViewModel.Game_PlayerChoice2 = cpuChoice;
        }

        public static void AdvancedMove(GameViewModel gameViewModel, Enum_Choice[] choices, List<GameResultInfo> gameResults, bool isPlayerOne)
        {
            int enemyRockCount = isPlayerOne ? gameResults.Count(r => r.PlayerChoice2 == Enum_Choice.Rock) : gameResults.Count(r => r.PlayerChoice1 == Enum_Choice.Rock);
            int enemyPaperCount = isPlayerOne ? gameResults.Count(r => r.PlayerChoice2 == Enum_Choice.Paper) : gameResults.Count(r => r.PlayerChoice1 == Enum_Choice.Paper);
            int enemyScissorsCount = isPlayerOne ? gameResults.Count(r => r.PlayerChoice2 == Enum_Choice.Scissors) : gameResults.Count(r => r.PlayerChoice1 == Enum_Choice.Scissors);

            int total = enemyRockCount + enemyPaperCount + enemyScissorsCount;

            Random random = new Random();
            int randomNumber = random.Next(total);

            //If human player has used rock 40% of the time, then CPU will choose paper 40% of the time. Repeated for each choice.

            if (isPlayerOne)
            {
                if (randomNumber < enemyRockCount)
                    gameViewModel.Game_PlayerChoice1 = Enum_Choice.Paper;
                else if (randomNumber < enemyRockCount + enemyPaperCount)
                    gameViewModel.Game_PlayerChoice1 = Enum_Choice.Scissors;
                else
                    gameViewModel.Game_PlayerChoice1 = Enum_Choice.Rock;
            }
            else
            {
                if (randomNumber < enemyRockCount)
                    gameViewModel.Game_PlayerChoice2 = Enum_Choice.Paper;
                else if (randomNumber < enemyRockCount + enemyPaperCount)
                    gameViewModel.Game_PlayerChoice2 = Enum_Choice.Scissors;
                else
                    gameViewModel.Game_PlayerChoice2 = Enum_Choice.Rock;
            }
        }

        public static GameResultInfo CreateGameResult(GameViewModel gameViewModel, int allResultsCount)
        {
            GameResultInfo gameResultInfo = new GameResultInfo();

            gameResultInfo.ResultForPlayer1 = gameViewModel.Game_ResultForPlayer1;
            gameResultInfo.CpuDifficulty1 = gameViewModel.Game_CpuDifficulty1;
            gameResultInfo.CpuDifficulty2 = gameViewModel.Game_CpuDifficulty2;
            gameResultInfo.PlayerChoice1 = gameViewModel.Game_PlayerChoice1.Value;
            gameResultInfo.PlayerChoice2 = gameViewModel.Game_PlayerChoice2.Value;
            gameResultInfo.IsCpuVsCpu = gameViewModel.Game_IsCpuVsCpu;
            gameResultInfo.Id = allResultsCount + 1;

            return gameResultInfo;
        }

        public static void SetGameResult(GameViewModel gameViewModel, int wins, int losses, int draws)
        {
            //Set Game Result
            Enum_Choice playerChoice1 = gameViewModel.Game_PlayerChoice1.Value;
            Enum_Choice playerChoice2 = gameViewModel.Game_PlayerChoice2.Value;

            bool isWin = playerChoice1 == Enum_Choice.Rock && playerChoice2 == Enum_Choice.Scissors ||
                playerChoice1 == Enum_Choice.Paper && playerChoice2 == Enum_Choice.Rock ||
                playerChoice1 == Enum_Choice.Scissors && playerChoice2 == Enum_Choice.Paper;

            bool isDraw = playerChoice1 == playerChoice2;

            if (isWin)
            {
                gameViewModel.Game_ResultForPlayer1 = Enum_Result.Win;
                wins++;
            }
            else if (isDraw)
            {
                gameViewModel.Game_ResultForPlayer1 = Enum_Result.Draw;
                draws++;
            }
            else
            {
                gameViewModel.Game_ResultForPlayer1 = Enum_Result.Loss;
                losses++;
            }

            gameViewModel.Game_Wins = wins;
            gameViewModel.Game_Losses = losses;
            gameViewModel.Game_Draws = draws;
        }
    }
}

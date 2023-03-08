using Microsoft.AspNetCore.Mvc;
using Rock_Paper_Scissors.ViewModels;
using static Rock_Paper_Scissors.Enums.Enums;
using Rock_Paper_Scissors.Models;
using Newtonsoft.Json;
using static Rock_Paper_Scissors.Helpers.GameHelpers;

namespace Rock_Paper_Scissors.Controllers
{
    public class GameController : Controller
    {

        public GameController()
        {
        }

        [HttpGet]
        public IActionResult Game(GameViewModel gameViewModel)
        {
            return View("Game", gameViewModel);
        }

        [HttpPost]
        public IActionResult Shoot(GameViewModel gameViewModel, string choice, int beginnerCount, bool showStatsHidden)
        {
            Enum_Choice[] choices = new Enum_Choice[] { Enum_Choice.Rock, Enum_Choice.Paper, Enum_Choice.Scissors };

            List<GameResultInfo> allResults = HttpContext.Session.GetString("GameResults") == null ? null : JsonConvert.DeserializeObject<List<GameResultInfo>>(HttpContext.Session.GetString("GameResults"));

            if (allResults == null)
                return View("Error");

            var results = allResults.FindAll(r => r.CpuDifficulty1 == gameViewModel.Game_CpuDifficulty1 && r.IsCpuVsCpu == false);

            int wins = results.Count(r => r.ResultForPlayer1 == Enum_Result.Win);
            int losses = results.Count(r => r.ResultForPlayer1 == Enum_Result.Loss);
            int draws = results.Count(r => r.ResultForPlayer1 == Enum_Result.Draw);

            //Set human player choice
            if (!Enum.TryParse<Enum_Choice>(choice, out Enum_Choice enum_choice))
                gameViewModel.Game_PlayerChoice1 = null;
            else
                gameViewModel.Game_PlayerChoice1 = enum_choice;

            switch (gameViewModel.Game_CpuDifficulty1)
            {
                case Enum_CpuDifficulty.Beginner:
                    {
                        BeginnerMove(beginnerCount, gameViewModel, choices, false, this);
                    }
                    break;
                case Enum_CpuDifficulty.Intermediate:
                    {
                        IntermediateMove(gameViewModel, choices, false);
                    }
                    break;
                case Enum_CpuDifficulty.Advanced:
                    {
                        AdvancedMove(gameViewModel, choices, results, false);
                    }
                    break;
                default:
                    return View("Error");
            }

            if (!gameViewModel.Game_PlayerChoice1.HasValue || !gameViewModel.Game_PlayerChoice2.HasValue)
                return View("Error");

            SetGameResult(gameViewModel, wins, losses, draws);

            //Save Results
            GameResultInfo gameResultInfo = CreateGameResult(gameViewModel, allResults.Count);

            allResults.Add(gameResultInfo);

            HttpContext.Session.SetString("GameResults", JsonConvert.SerializeObject(allResults));

            gameViewModel.Game_ShowStats = showStatsHidden;

            return PartialView("Game", gameViewModel);
        }

        [HttpPost]
        public IActionResult CpuMove(GameViewModel gameViewModel, int beginnerCount1, int beginnerCount2, bool showStatsHidden)
        {
            Enum_Choice[] choices = new Enum_Choice[] { Enum_Choice.Rock, Enum_Choice.Paper, Enum_Choice.Scissors };

            List<GameResultInfo> allResults = HttpContext.Session.GetString("GameResults") == null ? null : JsonConvert.DeserializeObject<List<GameResultInfo>>(HttpContext.Session.GetString("GameResults"));

            if (allResults == null)
                return View("Error");

            var results = allResults.FindAll(r => r.CpuDifficulty1.HasValue && r.CpuDifficulty1 == gameViewModel.Game_CpuDifficulty1 && r.CpuDifficulty2 == gameViewModel.Game_CpuDifficulty2 && r.IsCpuVsCpu);

            int wins = results.Count(r => r.ResultForPlayer1 == Enum_Result.Win);
            int losses = results.Count(r => r.ResultForPlayer1 == Enum_Result.Loss);
            int draws = results.Count(r => r.ResultForPlayer1 == Enum_Result.Draw);

            switch (gameViewModel.Game_CpuDifficulty1)
            {
                case Enum_CpuDifficulty.Beginner: 
                    {                       
                        BeginnerMove(beginnerCount1, gameViewModel, choices, true, this);
                    }
                    break;
                case Enum_CpuDifficulty.Intermediate:
                    {
                        IntermediateMove(gameViewModel, choices, true);
                    }
                    break;
                case Enum_CpuDifficulty.Advanced:
                    {
                        AdvancedMove(gameViewModel, choices, results, true);
                    }
                    break;
                default:
                    return View("Error");
            }

            switch (gameViewModel.Game_CpuDifficulty2)
            {
                case Enum_CpuDifficulty.Beginner:
                    {
                        BeginnerMove(beginnerCount2, gameViewModel, choices, false, this);
                    }
                    break;
                case Enum_CpuDifficulty.Intermediate:
                    {
                        IntermediateMove(gameViewModel, choices, false);
                    }
                    break;
                case Enum_CpuDifficulty.Advanced:
                    {
                        AdvancedMove(gameViewModel, choices, results, false);
                    }
                    break;
                default:
                    return View("Error");
            }

            if (!gameViewModel.Game_PlayerChoice1.HasValue || !gameViewModel.Game_PlayerChoice2.HasValue)
                return View("Error");

            SetGameResult(gameViewModel, wins, losses, draws);

            //Save Results
            GameResultInfo gameResultInfo = CreateGameResult(gameViewModel, allResults.Count);

            allResults.Add(gameResultInfo);

            HttpContext.Session.SetString("GameResults", JsonConvert.SerializeObject(allResults));

            gameViewModel.Game_ShowStats = showStatsHidden;

            return PartialView("Game", gameViewModel);
        }

        [HttpPost]
        public IActionResult ChangeSettings(GameViewModel gameViewModel)
        {
            return View("~/Views/Home/Index.cshtml", new IndexViewModel()
            {
                Index_CpuDifficulty1 = gameViewModel.Game_CpuDifficulty1,
                Index_CpuDifficulty2 = gameViewModel.Game_CpuDifficulty2,
                Index_IsCpuVsCpu = gameViewModel.Game_IsCpuVsCpu
            });
        }

        [HttpPost]
        public IActionResult ShowHideStats(GameViewModel gameViewModel, bool showStats)
        {
            gameViewModel.Game_ShowStats = showStats;

            return PartialView("Game", gameViewModel);
        }
    }
}


using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Rock_Paper_Scissors.Models;
using Rock_Paper_Scissors.ViewModels;
using System.Diagnostics;
using static Rock_Paper_Scissors.Enums.Enums;

namespace Rock_Paper_Scissors.Controllers
{
    public class HomeController : Controller
    {

        public HomeController()
        {
        }

        [HttpGet]
        public IActionResult Index()
        {
            IndexViewModel _indexViewModel = new IndexViewModel();

            return View(_indexViewModel);
        }

        [HttpPost]
        public IActionResult Index([Bind("Index_IsCpuVsCpu, Index_CpuDifficulty1, Index_CpuDifficulty2")] IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(indexViewModel);
            }

            bool isCpuVsCpu = indexViewModel.Index_IsCpuVsCpu.Value;

            Enum_CpuDifficulty difficulty1 = indexViewModel.Index_CpuDifficulty1.Value;
            Enum_CpuDifficulty? difficulty2 = isCpuVsCpu ? indexViewModel.Index_CpuDifficulty2.Value : null;

            if (HttpContext.Session.GetString("GameResults") == null)
            {
                var gameResults = new List<GameResultInfo>();
                HttpContext.Session.SetString("GameResults", JsonConvert.SerializeObject(gameResults));
            }

            List<GameResultInfo> allResults = JsonConvert.DeserializeObject<List<GameResultInfo>>(HttpContext.Session.GetString("GameResults"));

            var results = allResults.FindAll(r => r.CpuDifficulty1 == difficulty1 && r.CpuDifficulty2 == difficulty2 && r.IsCpuVsCpu == isCpuVsCpu);

            int wins = results.Count(r => r.ResultForPlayer1 == Enum_Result.Win);
            int losses = results.Count(r => r.ResultForPlayer1 == Enum_Result.Loss);
            int draws = results.Count(r => r.ResultForPlayer1 == Enum_Result.Draw);

            return View("Game", new GameViewModel() { Game_IsCpuVsCpu = isCpuVsCpu, Game_CpuDifficulty1 = difficulty1, Game_CpuDifficulty2 = difficulty2, Game_Wins = wins, Game_Losses = losses, Game_Draws = draws });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

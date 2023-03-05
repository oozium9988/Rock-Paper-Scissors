
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Rock_Paper_Scissors.ViewModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace Chess.Controllers
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
        public IActionResult Index([Bind("IsCpuVsCpu, Cpu1Difficulty, Cpu2Difficulty")] IndexViewModel indexViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(indexViewModel);
            }

            bool isCpuVsCpu = indexViewModel.IsCpuVsCpu;
            string difficulty1 = indexViewModel.Cpu1Difficulty;
            string difficulty2 = indexViewModel.Cpu2Difficulty;

            return View("Game", new GameViewModel { IsCpuVsCpu = isCpuVsCpu, Cpu1Difficulty = difficulty1, Cpu2Difficulty = difficulty2 });
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

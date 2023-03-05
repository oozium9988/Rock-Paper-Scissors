using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Rock_Paper_Scissors.ViewModels;

namespace Rock_Paper_Scissors.Controllers
{
    public class GameController : Controller
    {

        public GameController()
        { 
        }

        [HttpGet]
        public IActionResult Game(GameViewModel GameViewModel)
        {
            return View(GameViewModel);
        }

        [HttpPost]
        public IActionResult Shoot(GameViewModel gameViewModel, string choice)
        {
            string[] choices = new string[] { "rock", "paper", "scissors" };

            Random random = new Random();
            int randomNumber = random.Next(3);

            string cpuChoice = choices[randomNumber];

            gameViewModel.Player1Choice = choice;
            gameViewModel.Player2Choice = cpuChoice;

            return PartialView("Game", gameViewModel);
        }
    }
}

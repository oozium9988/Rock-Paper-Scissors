using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rock_Paper_Scissors.ViewModels
{
    public class GameViewModel
    {
        public bool IsCpuVsCpu { get; set; }
        public string Cpu1Difficulty { get; set; }
        public string? Cpu2Difficulty { get; set; }
        public string? Player1Choice { get; set; }
        public string? Player2Choice { get; set; }
    }          
}

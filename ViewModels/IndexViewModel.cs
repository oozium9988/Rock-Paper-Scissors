using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Rock_Paper_Scissors.ViewModels
{
    public class IndexViewModel
    {
        [Required]
        public bool IsCpuVsCpu { get; set; }
        [Required]
        public string Cpu1Difficulty { get; set; }
        [RequiredIfCpuVsCpu]
        public string? Cpu2Difficulty { get; set; }
    }
}

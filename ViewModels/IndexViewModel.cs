using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using static Rock_Paper_Scissors.Enums.Enums;

namespace Rock_Paper_Scissors.ViewModels
{
    public class IndexViewModel
    {
        [Required(ErrorMessage = "Please select Me vs CPU or CPU vs CPU.")]
        public bool? Index_IsCpuVsCpu { get; set; }
        [Required(ErrorMessage = "Please select the difficulty for CPU 1.")]
        public Enum_CpuDifficulty? Index_CpuDifficulty1 { get; set; }
        [RequiredIfCpuVsCpu(ErrorMessage = "Please select the difficulty for CPU 2.")]
        public Enum_CpuDifficulty? Index_CpuDifficulty2 { get; set; }
    }
}

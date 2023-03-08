using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rock_Paper_Scissors.Enums
{
    public static class Enums
    {
        public enum Enum_Result
        {
            Win = 1,
            Draw,
            Loss
        }

        public enum Enum_CpuDifficulty
        {
            Beginner = 1,
            Intermediate,
            Advanced
        }

        public enum Enum_Choice
        {
            Rock = 1,
            Paper,
            Scissors
        }
    }
}

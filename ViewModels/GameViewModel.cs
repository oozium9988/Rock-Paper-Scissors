using static Rock_Paper_Scissors.Enums.Enums;

namespace Rock_Paper_Scissors.ViewModels
{
    public class GameViewModel
    {
        public bool Game_IsCpuVsCpu { get; set; }
        public Enum_CpuDifficulty Game_CpuDifficulty1 { get; set; }
        public Enum_CpuDifficulty? Game_CpuDifficulty2 { get; set; }
        public Enum_Choice? Game_PlayerChoice1 { get; set; }
        public Enum_Choice? Game_PlayerChoice2 { get; set; }
        public int Game_BeginnerCpuMoveCount1 { get; set; }
        public int Game_BeginnerCpuMoveCount2 { get; set; }
        public Enum_Result Game_ResultForPlayer1 { get; set; }
        public int Game_Wins { get; set; }
        public int Game_Draws { get; set; }
        public int Game_Losses { get; set; }
        public bool Game_ShowStats { get; set; }
    }
}

using static Rock_Paper_Scissors.Enums.Enums;

namespace Rock_Paper_Scissors.Models
{
    public class GameResultInfo
    {
        public int Id { get; set; }
        public bool IsCpuVsCpu { get; set; }
        public Enum_CpuDifficulty? CpuDifficulty1 { get; set; }
        public Enum_CpuDifficulty? CpuDifficulty2 { get; set; }
        public Enum_Choice PlayerChoice1 { get; set; }
        public Enum_Choice PlayerChoice2 { get; set; }
        public Enum_Result ResultForPlayer1 { get; set; }
    }
}

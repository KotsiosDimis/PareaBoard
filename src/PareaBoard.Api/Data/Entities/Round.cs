namespace PareaBoard.Api.Data.Entities
{
    public class Round
    {
        public int Id { get; set; }

        public int GameId { get; set; }
        public Game Game { get; set; } = null!;

        public int Number { get; set; }     // 1, 2, 3 … 19
        public int CardCount { get; set; }  // 1, 2 … 10 … 2, 1

        public List<RoundScore> Scores { get; set; } = [];
    }
}

namespace PareaBoard.Api.Data.Entities
{
    public class RoundScore
    {
        public int Id { get; set; }

        public int RoundId { get; set; }
        public Round Round { get; set; } = null!;

        public int GamePlayerId { get; set; }
        public GamePlayer GamePlayer { get; set; } = null!;

        public int Points { get; set; }
    }
}

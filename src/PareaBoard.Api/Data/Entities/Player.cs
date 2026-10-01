namespace PareaBoard.Api.Data.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public bool IsActive { get; set; } = true;
        public DateTime CreatedAt { get; set; }

        public List<GamePlayer> GamePlayers { get; set; } = [];
    }
}

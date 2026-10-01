using System.Security.AccessControl;

namespace PareaBoard.Api.Data.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public GameType Type { get; set; }
        public GameStatus Status { get; set; } = GameStatus.InProgress;

        public DateTime PlayedAt { get; set; }
        public DateTime? FinishedAt { get; set; }
        public string? Notes { get; set; }

        // Prediction setting: the cap chosen for this game (e.g. 10).
        // Null for game types that don't use it.
        public int? MaxCards { get; set; }

        public List<GamePlayer> Players { get; set; } = [];
        public List<Round> Rounds { get; set; } = [];
    }

    public enum GameType
    {
        Prediction = 1,
        Tichu = 2
    }

    public enum GameStatus
    {
        InProgress = 1,
        Finished = 2,
        Abandoned = 3
    }
}

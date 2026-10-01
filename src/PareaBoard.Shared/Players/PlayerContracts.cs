namespace PareaBoard.Shared.Players
{
    public record PlayerDto(int Id, string Name, bool IsActive);

    public record CreatePlayerRequest(string Name);

    public record UpdatePlayerRequest(string Name, bool IsActive);
}

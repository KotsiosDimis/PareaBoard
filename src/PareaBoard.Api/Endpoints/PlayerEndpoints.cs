using Microsoft.AspNetCore.Http.HttpResults;
using PareaBoard.Api.Data;
using PareaBoard.Api.Data.Entities;
using PareaBoard.Shared.Players;

namespace PareaBoard.Api.Endpoints
{
    public static class PlayerEndpoints
    {
        public static void MapPlayerEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("/api/players");

            group.MapGet("/", GetAll);
            group.MapGet("/{id:int}", GetById);
            group.MapPost("/", Create);
            group.MapPut("/{id:int}", Update);
        }

        static async Task<Ok<List<PlayerDto>>> GetAll(
            PareaBoardDbContext db, bool includeInactive = false)
        {
            var players = await db.Players
                .Where(p => includeInactive || p.IsActive)
                .OrderBy(p => p.Name)
                .Select(p => new PlayerDto(p.Id, p.Name, p.IsActive))
                .ToListAsync();

            return TypedResults.Ok(players);
        }

        static async Task<Results<Ok<PlayerDto>, NotFound>> GetById(
            int id, PareaBoardDbContext db)
        {
            var player = await db.Players.FindAsync(id);
            return player is null
                ? TypedResults.NotFound()
                : TypedResults.Ok(player.ToDto());
        }

        static async Task<Results<Created<PlayerDto>, ValidationProblem, Conflict<string>>> Create(
            CreatePlayerRequest request, PareaBoardDbContext db)
        {
            var name = request.Name?.Trim() ?? "";

            if (name.Length is 0 or > 50)
                return TypedResults.ValidationProblem(new Dictionary<string, string[]>
                {
                    ["name"] = ["Name must be 1–50 characters."]
                });

            if (await db.Players.AnyAsync(p => p.Name == name))
                return TypedResults.Conflict($"A player named '{name}' already exists.");

            var player = new Player { Name = name, CreatedAt = DateTime.UtcNow };
            db.Players.Add(player);
            await db.SaveChangesAsync();

            return TypedResults.Created($"/api/players/{player.Id}", player.ToDto());
        }

        static async Task<Results<Ok<PlayerDto>, NotFound, ValidationProblem, Conflict<string>>> Update(
            int id, UpdatePlayerRequest request, PareaBoardDbContext db)
        {
            // Your turn: find the player, validate the name like Create,
            // check the name isn't taken by a *different* player,
            // update Name and IsActive, save, return the DTO.
            throw new NotImplementedException();
        }

        static PlayerDto ToDto(this Player p) => new(p.Id, p.Name, p.IsActive);
    }
}

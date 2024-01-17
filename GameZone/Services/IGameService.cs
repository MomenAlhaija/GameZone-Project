using GameZone.Models;

namespace GameZone.Services
{
    public interface IGameService
    {
       Task GreateGame(CreateGameFormViewModel input);
        IEnumerable<Game> GetGames();

    }
}

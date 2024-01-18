using GameZone.Models;

namespace GameZone.Services
{
    public interface IGameService
    {
        Task GreateGame(CreateGameFormViewModel input);
        IEnumerable<Game> GetAllGames();
        Task<Game?> GetGame(int  id);    
        Task EditGame(EditGameFormViewModel input);
        Task<bool> DeleteGame(int id);
    }
}

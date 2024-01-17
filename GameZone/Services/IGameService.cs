namespace GameZone.Services
{
    public interface IGameService
    {
       Task GreateGame(CreateGameFormViewModel input);
    }
}

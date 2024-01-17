using GameZone.Models;
using GameZone.Settings;

namespace GameZone.Services
{
    public class GameService : IGameService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly string _imagePath;
        public GameService(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _imagePath = $"{_webHostEnvironment.WebRootPath}{FileSettings.FilePath}";
        }
        public async Task GreateGame(CreateGameFormViewModel input)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(input.Cover.FileName)}";
            var path=Path.Combine(_imagePath, coverName);
            using var stream=File.Create(path);
            await input.Cover.CopyToAsync(stream);

            Game game = new()
            {
                Name=input.Name,
                Description=input.Description,
                CategoryId=input.CategoryId,
                Cover=coverName,
                Devices=input.SelectedDevices.Select(d=>new GameDevice { DeviceId=d}).ToList()
            };
            _context.Add(game);
        }
    }
}

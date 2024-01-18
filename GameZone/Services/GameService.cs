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

        public async Task<Game?> GetGame(int id)
        {
            return await _context.Games
                   .Include(c=>c.Category)
                   .Include(c=>c.Devices).
                    ThenInclude(c=>c.Device)
                   .SingleOrDefaultAsync(x=>x.Id==id);
        }

        public IEnumerable<Game> GetAllGames()
        {
            return _context.Games.
                Include(c=>c.Category).
                Include(c=>c.Devices).
                ThenInclude(c=>c.Device)
                .AsNoTracking().ToList();
        }

        public async Task GreateGame(CreateGameFormViewModel input)
        {
            var coverName =await SaveCover(input.Cover);
            Game game = new()
            {
                Name=input.Name,
                Description=input.Description,
                CategoryId=input.CategoryId,
                Cover=coverName,
                Devices=input.SelectedDevices.Select(d=>new GameDevice { DeviceId=d}).ToList()
            };
            _context.Add(game);
            _context.SaveChanges();
        }

        public async Task EditGame(EditGameFormViewModel input)
        {
            var game = await _context.Games.Include(g=>g.Devices).ThenInclude(c=>c.Device).FirstOrDefaultAsync(x=>x.Id==input.Id);
            if (game is null ) 
                throw new Exception("Not Found The Game");
            game.Name = input.Name;
            game.Description=input.Description;
            game.Devices = input.SelectedDevices.Select(d=>new GameDevice() { DeviceId=d }).ToList();
            game.CategoryId = input.CategoryId;
            var oldGameName=game.Cover;  
            if(input.Cover is not null)
                game.Cover=await SaveCover(input.Cover);
            _context.Update(game);
            var rowsAffected= _context.SaveChanges();
            if(rowsAffected>0 && input.Cover is not null)
                File.Delete(Path.Combine(_imagePath, oldGameName));
            else
                File.Delete(Path.Combine(_imagePath, game.Cover));
        }


        public async Task<bool> DeleteGame(int id)
        {
            bool isDeleted = false;
            var game = await _context.Games.FindAsync(id);
            if (game is null)
                return isDeleted;
            _context.Games.Remove(game);
            var affectedRows=_context.SaveChanges();
            if (affectedRows > 0) {

                isDeleted = true;
                File.Delete(Path.Combine(_imagePath,game.Name));
            };
            return isDeleted;
        }


        #region Private Method
        private async Task<string> SaveCover(IFormFile cover)
        {
            var coverName = $"{Guid.NewGuid()}{Path.GetExtension(cover.FileName)}";
            var path = Path.Combine(_imagePath, coverName);
            using var stream = File.Create(path);
            await cover.CopyToAsync(stream);
            return coverName;
        }
        #endregion

    }
}

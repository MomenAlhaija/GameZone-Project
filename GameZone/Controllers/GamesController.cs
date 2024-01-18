using GameZone.Services;
using Microsoft.AspNetCore.Mvc;

namespace GameZone.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IDeviceService _deviceService;
        private readonly IGameService _gameService;
        public GamesController(ICategoryService categoryService, IDeviceService deviceService, IGameService gameService)
        {
            _categoryService = categoryService;
            _deviceService = deviceService;
            _gameService = gameService;

        }
        public IActionResult Index()
        {
            var game=_gameService.GetAllGames();
            return View(game);
        }

        [HttpGet]
        public IActionResult AddGame()
        {
            CreateGameFormViewModel model = new()
            {
                Categories = _categoryService.GetSelectListCategories(),
                Devices = _deviceService.GetSelectListDevices(),
            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> AddGame(CreateGameFormViewModel input)
        {
            if (!ModelState.IsValid)
            {
                input.Categories = _categoryService.GetSelectListCategories();
                input.Devices= _deviceService.GetSelectListDevices();
                return View(input);
            }
            await _gameService.GreateGame(input);
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var game= await _gameService.GetGame(id);
            return View(game);  
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var game =await _gameService.GetGame(id);
            if (game is null)
                throw new Exception("The Game Not Found");
            EditGameFormViewModel viewModel = new EditGameFormViewModel()
            {
                Id=id,
                Name=game.Name,
                Description=game.Description,
                CategoryId=game.CategoryId,
                SelectedDevices=game.Devices.Select(d=>d.DeviceId).ToList(),
                Categories=_categoryService.GetSelectListCategories(),
                Devices=_deviceService.GetSelectListDevices(),
                CurrentCover=game.Cover,
            }; 
            return View(viewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(EditGameFormViewModel input)
        {
            if (!ModelState.IsValid)
            {
                input.Categories = _categoryService.GetSelectListCategories();
                input.Devices = _deviceService.GetSelectListDevices();
                return View(input);
            }
            await _gameService.EditGame(input);
            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var isDeleted=await _gameService.DeleteGame(id);
            return isDeleted? Ok():BadRequest();
        }
    }
}

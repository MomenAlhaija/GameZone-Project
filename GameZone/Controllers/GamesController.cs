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
            return View();
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
    }
}

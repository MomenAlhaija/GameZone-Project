namespace GameZone.ViewModel
{
    public class CreateGameFormViewModel:GameFormModel
    {
        public IFormFile Cover { get; set; } = default!;
    }
}

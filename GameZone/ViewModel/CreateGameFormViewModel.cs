using GameZone.Attributes;
using GameZone.Settings;

namespace GameZone.ViewModel
{
    public class CreateGameFormViewModel:GameFormModel
    {
        [AllowedExtension(FileSettings.AllowedExtensions)]
        [MaxFileSize(FileSettings.MaxFileSizeInBytes)]
        public IFormFile Cover { get; set; } = default!;
    }
}

using GameZone.Attributes;
using GameZone.Consts;
using GameZone.Settings;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModel
{
    public class CreateGameFormViewModel:GameFormModel
    {
        [AllowedExtension(FileSetting.AllowedExtensions)]
        public IFormFile Cover { get; set; } = default!;
    }
}

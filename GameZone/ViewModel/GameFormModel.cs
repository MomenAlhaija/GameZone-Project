using GameZone.Consts;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace GameZone.ViewModel
{
    public class GameFormModel
    {
        [MaxLength(GameZoneConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;
        [Display(Name="Category Name")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();
        [Display(Name = "Support Devices")]
        public List<int> SelectedDevices { get; set; } = default!;
        public IEnumerable<SelectListItem> Devices { get; set; } = Enumerable.Empty<SelectListItem>();


        [MaxLength(GameZoneConsts.MaxGameDescriptionLength)]
        public string Description { get; set; } = string.Empty;

    }
}

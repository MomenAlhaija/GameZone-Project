using GameZone.Consts;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class Device:BaseEntity
    {
        [MaxLength(GameZoneConsts.MaxDeviceIconLength)]
        public string Icon { get; set; }=string.Empty;
    }
}

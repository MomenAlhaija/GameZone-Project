using GameZone.Consts;
using System.ComponentModel.DataAnnotations;

namespace GameZone.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [MaxLength(GameZoneConsts.MaxNameLength)]
        public string Name { get; set; } = string.Empty;
    }
}

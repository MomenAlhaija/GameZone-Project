using GameZone.Consts;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class Game:BaseEntity
    {
        [MaxLength(GameZoneConsts.MaxGameDescriptionLength)]
        public string Description { get; set; }= string.Empty;
        public string Cover { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<GameDevice> Devices { get; set; } =new List<GameDevice>();      
    }
}

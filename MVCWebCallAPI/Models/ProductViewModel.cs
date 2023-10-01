using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MVCWebCallAPI.Models
{
    public class ProductViewModel
    {
        public int ProductId { get; set; }
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public decimal? UnitPrice { get; set; }
        [Required]
        public int? UnitsInStock { get; set; }
        public string? Image { get; set; }
        [Required]
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual CategoryViewModel? Category { get; set; }
    }
}

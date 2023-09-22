//using APIWithDatabase.Models;
using System.ComponentModel.DataAnnotations;

namespace APIWithDatabase.Data
{
    public class AddProduct
    {
        [Required]
        public string? ProductName { get; set; }
        [Required]
        public decimal? UnitPrice { get; set; }
        [Required]
        [Range(1,1000)]
        public int? UnitsInStock { get; set; }
        public string? Image { get; set; }
        [Required]
        public int? CategoryId { get; set; }
        
    }
}

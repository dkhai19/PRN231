using System.Text.Json.Serialization;

namespace MVCWebCallAPI.Models
{
    public class CategoryViewModel
    {
        public CategoryViewModel()
        {
            Products = new HashSet<ProductViewModel>();
        }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        [JsonIgnore]
        public virtual ICollection<ProductViewModel> Products { get; set; }
    }
}

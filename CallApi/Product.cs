using CallApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CallApi
{
    public class Product
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
        public string? Image { get; set; }
        public int? CategoryId { get; set; }

        [JsonIgnore]
        public virtual Category? Category { get; set; }

        public override string? ToString()
        {
            return ProductId + "\t" + ProductName + "\t" + UnitPrice + "\t" + UnitsInStock + "\t" + Image + "\t" + CategoryId;
        }
    }
}

using System.ComponentModel.DataAnnotations;
using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class ProductBasicInfoPostDTO : IAddible<Product>
    {
        [Required]
        [StringLength(40)]
        public string Name { get; set; }

        [Required]
        public bool IsDiscontinued { get; set; }

        [Range(0, float.PositiveInfinity)]
        public decimal? Price { get; set; }


        public Product GetDataBaseObject()
        {
            return new Product()
            {
                ProductName = Name,
                Discontinued = IsDiscontinued,
                UnitPrice = Price
            };
        }
    }
}

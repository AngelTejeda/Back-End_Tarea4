using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class ProductBasicInfoDTO : ProductDTO
    {
        public decimal? Price { get; set; }

        public ProductBasicInfoDTO()
        {

        }
        public ProductBasicInfoDTO(string name, bool isDiscontinued, decimal price)
        {
            Name = name;
            IsDiscontinued = isDiscontinued;
            Price = price;
        }

        public ProductBasicInfoDTO(Product product)
        {
            Name = product.ProductName;
            IsDiscontinued = product.Discontinued;
            Price = product.UnitPrice;
        }

        public override Product GetDataBaseProductObject()
        {
            return new Product()
            {
                ProductName = Name,
                Discontinued = IsDiscontinued,
                UnitPrice = Price
            };
        }

        public override void ModifyDataBaseProduct(Product dataBaseProduct)
        {
            dataBaseProduct.ProductName = Name;
            dataBaseProduct.Discontinued = IsDiscontinued;
            dataBaseProduct.UnitPrice = Price;
        }
    }
}

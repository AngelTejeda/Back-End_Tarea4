using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class ProductBasicInfoDTO : ProductDTO
    {
        public decimal? Price { get; set; }

        public ProductBasicInfoDTO()
        {

        }

        public ProductBasicInfoDTO(Product dataBaseProduct)
        {
            CopyInfoFromDataBaseProduct(dataBaseProduct);
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

        public override void CopyInfoFromDataBaseProduct(Product dataBaseProduct)
        {
            Name = dataBaseProduct.ProductName;
            IsDiscontinued = dataBaseProduct.Discontinued;
            Price = dataBaseProduct.UnitPrice;
        }
    }
}

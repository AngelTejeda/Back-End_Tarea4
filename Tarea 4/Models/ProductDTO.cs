using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public abstract class ProductDTO
    {
        public string Name { get; set; }
        public bool IsDiscontinued { get; set; }

        public abstract Product GetDataBaseProductObject();
        public abstract void ModifyDataBaseProduct(Product dataBaseProduct);
    }
}
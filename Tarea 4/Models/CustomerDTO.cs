using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public abstract class CustomerDTO
    {
        public string Id { get; set; }
        public string Company { get; set; }

        public abstract Customer GetDataBaseCustomerObject();
        public abstract void ModifyDataBaseCustomer(Customer dataBaseCustomer);
        public abstract void CopyInfoFromDataBaseCustomer(Customer dataBaseCustomer);
    }
}

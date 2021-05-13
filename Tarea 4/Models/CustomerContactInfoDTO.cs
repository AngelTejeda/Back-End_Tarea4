using System.ComponentModel.DataAnnotations;
using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class CustomerContactInfoDTO : IReadable<Customer>
    {
        public string Id { get; set; }
        public string Company { get; set; }
        public string ContactFullName { get; set; }
        public string ContactPosition { get; set; }
        public string ContactPhone { get; set; }

        public CustomerContactInfoDTO()
        {

        }

        public CustomerContactInfoDTO(Customer dataBaseCustomer)
        {
            CopyInfoFromDataBaseObject(dataBaseCustomer);
        }

        public void CopyInfoFromDataBaseObject(Customer dataBaseObject)
        {
            Id = dataBaseObject.CustomerId;
            Company = dataBaseObject.CompanyName;
            ContactFullName = dataBaseObject.ContactName;
            ContactPosition = dataBaseObject.ContactTitle;
            ContactPhone = dataBaseObject.Phone;
        }
    }
}

using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class CustomerContactInfoDTO : CustomerDTO
    {
        public string ContactFullName { get; set; }
        public string ContactPosition { get; set; }
        public string ContactPhone { get; set; }

        public CustomerContactInfoDTO()
        {

        }

        public CustomerContactInfoDTO(Customer dataBaseCustomer)
        {
            CopyInfoFromDataBaseCustomer(dataBaseCustomer);
        }

        public override Customer GetDataBaseCustomerObject()
        {
            return new Customer()
            {
                CustomerId = Id,
                CompanyName = Company,
                ContactName = ContactFullName,
                ContactTitle = ContactPosition,
                Phone = ContactPhone
        };
        }

        public override void ModifyDataBaseCustomer(Customer dataBaseCustomer)
        {
            dataBaseCustomer.CustomerId = Id;
            dataBaseCustomer.CompanyName = Company;
            dataBaseCustomer.ContactName = ContactFullName;
            dataBaseCustomer.ContactTitle = ContactPosition;
            dataBaseCustomer.Phone = ContactPhone;
        }

        public override void CopyInfoFromDataBaseCustomer(Customer dataBaseCustomer)
        {
            Id = dataBaseCustomer.CustomerId;
            Company = dataBaseCustomer.CompanyName;
            ContactFullName = dataBaseCustomer.ContactName;
            ContactPosition = dataBaseCustomer.ContactTitle;
            ContactPhone = dataBaseCustomer.Phone;
        }
    }
}

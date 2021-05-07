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

        public CustomerContactInfoDTO(string id, string company, string contactFullName, string contactPosition, string contactPhone)
        {
            Id = id;
            Company = company;
            ContactFullName = contactFullName;
            ContactPosition = contactPosition;
            ContactPhone = contactPhone;
        }

        public CustomerContactInfoDTO(Customer customer)
        {
            Id = customer.CustomerId;
            Company = customer.CompanyName;
            ContactFullName = customer.ContactName;
            ContactPosition = customer.ContactTitle;
            ContactPhone = customer.Phone;
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
    }
}

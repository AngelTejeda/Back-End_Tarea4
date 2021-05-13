using System.ComponentModel.DataAnnotations;
using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class CustomerContactInfoPostDTO : IAddible<Customer>
    {
        [Required]
        [StringLength(5)]
        public string Id { get; set; }

        [Required]
        [StringLength(40)]
        public string Company { get; set; }

        [StringLength(30)]
        public string ContactFullName { get; set; }

        [StringLength(30)]
        public string ContactPosition { get; set; }

        [StringLength(24)]
        public string ContactPhone { get; set; }


        public Customer GetDataBaseObject()
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

        public void ModifyDataBaseObject(Customer dataBaseObject)
        {
            dataBaseObject.CustomerId = Id;
            dataBaseObject.CompanyName = Company;
            dataBaseObject.ContactName = ContactFullName;
            dataBaseObject.ContactTitle = ContactPosition;
            dataBaseObject.Phone = ContactPhone;
        }
    }
}

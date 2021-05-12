using System.ComponentModel.DataAnnotations;
using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class EmployeeContactInfoDTO : EmployeeDTO
    {
        [StringLength(60)]
        public string HomeAddress { get; set; }

        [StringLength(15)]
        public string HomeCity { get; set; }

        [StringLength(15)]
        public string Countryland { get; set; }

        [StringLength(24)]
        public string PhoneNumber { get; set; }

        public EmployeeContactInfoDTO()
        {

        }

        public EmployeeContactInfoDTO(Employee dataBaseEmployee)
        {
            CopyInfoFromDataBaseEmployee(dataBaseEmployee);
        }

        public override Employee GetDataBaseEmployeeObject()
        {
            return new Employee()
            {
                FirstName = Name,
                LastName = FamilyName,
                Address = HomeAddress,
                City = HomeCity,
                Country = Countryland,
                HomePhone = PhoneNumber
            };
        }

        public override void ModifyDataBaseEmployee(Employee dataBaseEmployee)
        {
            dataBaseEmployee.FirstName = Name;
            dataBaseEmployee.LastName = FamilyName;
            dataBaseEmployee.Address = HomeAddress;
            dataBaseEmployee.City = HomeCity;
            dataBaseEmployee.Country = Countryland;
            dataBaseEmployee.HomePhone = PhoneNumber;
        }

        public override void CopyInfoFromDataBaseEmployee(Employee dataBaseEmployee)
        {
            Name = dataBaseEmployee.FirstName;
            FamilyName = dataBaseEmployee.LastName;
            HomeAddress = dataBaseEmployee.Address;
            HomeCity = dataBaseEmployee.City;
            Countryland = dataBaseEmployee.Country;
            PhoneNumber = dataBaseEmployee.HomePhone;
        }
    }
}
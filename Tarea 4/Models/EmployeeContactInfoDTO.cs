using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class EmployeeContactInfoDTO : EmployeeDTO
    {
        public string HomeAddress { get; set; }
        public string HomeCity { get; set; }
        public string Countryland { get; set; }
        public string PhoneNumber { get; set; }

        public EmployeeContactInfoDTO()
        {

        }

        public EmployeeContactInfoDTO(string homeAddress, string homeCity, string countryland, string phoneNumber)
        {
            HomeAddress = homeAddress;
            HomeCity = homeCity;
            Countryland = countryland;
            PhoneNumber = phoneNumber;
        }

        public EmployeeContactInfoDTO(Employee employee)
        {
            Name = employee.FirstName;
            FamilyName = employee.LastName;
            HomeAddress = employee.Address;
            HomeCity = employee.City;
            Countryland = employee.Country;
            PhoneNumber = employee.HomePhone;
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
    }
}
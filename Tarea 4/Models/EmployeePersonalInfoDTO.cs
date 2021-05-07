using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class EmployeePersonalInfoDTO : EmployeeDTO
    {
        public int Id { get; set; }
        public string HomeAddress { get; set; }

        public EmployeePersonalInfoDTO()
        {

        }

        public EmployeePersonalInfoDTO(string name, string familyName, int id, string homeAddress)
        {
            Name = name;
            FamilyName = familyName;
            Id = id;
            HomeAddress = homeAddress;
        }

        public EmployeePersonalInfoDTO(Employee employee)
        {
            Name = employee.FirstName;
            FamilyName = employee.LastName;
            Id = employee.EmployeeId;
            HomeAddress = employee.Address;
        }

        public override Employee GetDataBaseEmployeeObject()
        {
            return new Employee()
            {
                FirstName = Name,
                LastName = FamilyName,
                EmployeeId = Id,
                Address = HomeAddress
            };
        }

        public override void ModifyDataBaseEmployee(Employee dataBaseEmployee)
        {
            dataBaseEmployee.FirstName = Name;
            dataBaseEmployee.LastName = FamilyName;
            dataBaseEmployee.EmployeeId = Id;
            dataBaseEmployee.Address = HomeAddress;
        }
    }
}
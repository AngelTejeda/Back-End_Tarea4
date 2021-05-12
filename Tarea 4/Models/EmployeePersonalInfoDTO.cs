using System.ComponentModel.DataAnnotations;
using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class EmployeePersonalInfoDTO : EmployeeDTO
    {
        public int Id { get; set; }

        [StringLength(60)]
        public string HomeAddress { get; set; }

        public EmployeePersonalInfoDTO()
        {

        }

        public EmployeePersonalInfoDTO(Employee dataBaseEmployee)
        {
            CopyInfoFromDataBaseEmployee(dataBaseEmployee);
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

        public override void CopyInfoFromDataBaseEmployee(Employee dataBaseEmployee)
        {
            Name = dataBaseEmployee.FirstName;
            FamilyName = dataBaseEmployee.LastName;
            Id = dataBaseEmployee.EmployeeId;
            HomeAddress = dataBaseEmployee.Address;
        }
    }
}
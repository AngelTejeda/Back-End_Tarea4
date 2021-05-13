using System.ComponentModel.DataAnnotations;
using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class EmployePersonalInfoPostPutDTO : IUpdatable<Employee>, IAddible<Employee>
    {
        [Required]
        [StringLength(10)]
        public string Name { get; set; }

        [Required]
        [StringLength(20)]
        public string FamilyName { get; set; }

        [StringLength(60)]
        public string HomeAddress { get; set; }


        public Employee GetDataBaseObject()
        {
            return new Employee()
            {
                FirstName = Name,
                LastName = FamilyName,
                Address = HomeAddress
            };
        }

        public void ModifyDataBaseObject(Employee dataBaseObject)
        {
            dataBaseObject.FirstName = Name;
            dataBaseObject.LastName = FamilyName;
            dataBaseObject.Address = HomeAddress;
        }
    }
}

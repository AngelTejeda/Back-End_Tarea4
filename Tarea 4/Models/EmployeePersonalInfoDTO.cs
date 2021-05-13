using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public class EmployeePersonalInfoDTO : IReadable<Employee>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FamilyName { get; set; }
        public string HomeAddress { get; set; }

        public EmployeePersonalInfoDTO()
        {

        }

        public EmployeePersonalInfoDTO(Employee dataBaseEmployee)
        {
            CopyInfoFromDataBaseObject(dataBaseEmployee);
        }

        public void CopyInfoFromDataBaseObject(Employee dataBaseObject)
        {
            Id = dataBaseObject.EmployeeId;
            Name = dataBaseObject.FirstName;
            FamilyName = dataBaseObject.LastName;
            HomeAddress = dataBaseObject.Address;
        }
    }
}

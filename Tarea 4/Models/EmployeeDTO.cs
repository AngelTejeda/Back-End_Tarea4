using Tarea_4.DataAccess;

namespace Tarea_4.Models
{
    public abstract class EmployeeDTO
    {
        public string Name { get; set; }
        public string FamilyName { get; set; }

        public abstract Employee GetDataBaseEmployeeObject();
        public abstract void ModifyDataBaseEmployee(Employee dataBaseEmployee);
    }
}
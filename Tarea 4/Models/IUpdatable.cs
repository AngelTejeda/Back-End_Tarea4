namespace Tarea_4.Models
{
    public interface IUpdatable<T> where T: class
    {
        public void ModifyDataBaseObject(T dataBaseObject);
    }
}

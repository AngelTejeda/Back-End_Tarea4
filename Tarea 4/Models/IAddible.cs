namespace Tarea_4.Models
{
    public interface IAddible<T> where T: class
    {
        public abstract T GetDataBaseObject();
    }
}

namespace Tarea_4.Models
{
    public interface IReadable<T> where T: class
    {
        public void CopyInfoFromDataBaseObject(T dataBaseObject);
    }
}

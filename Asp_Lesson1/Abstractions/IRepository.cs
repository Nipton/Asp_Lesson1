namespace Asp_Lesson1.Abstractions
{
    public interface IRepository<T> where T : class
    {
        int Create(T item);
        IEnumerable<T> GetAll();
        void Update(T item);
        void Delete(int id);

    }
}

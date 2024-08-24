using Asp_Lesson1.Repositories;

namespace Asp_Lesson1.Abstractions
{
    public interface IUnitOfWork : IDisposable
    {
        ProductRepository Products { get; }
        ProductGroupRepository GroupProduct { get; }
    }
}

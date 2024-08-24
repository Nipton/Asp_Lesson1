using Asp_Lesson1.Abstractions;
using AutoMapper;

namespace Asp_Lesson1.Repositories
{
    public class UnitOfWork : IDisposable, IUnitOfWork
    {
        private ProductsContext db;
        private IMapper mapper;
        private bool disposedValue;
        private ProductRepository productRepository;
        private ProductGroupRepository productGroupRepository;

        public ProductRepository Products
        {
            get
            {
                if (productRepository == null)
                    productRepository = new ProductRepository(db, mapper);
                return productRepository;
            }
        }
        public ProductGroupRepository GroupProduct
        {
            get
            {
                if (productGroupRepository == null)
                    productGroupRepository = new ProductGroupRepository(db, mapper);
                return productGroupRepository;
            }
        }
        public UnitOfWork(IMapper mapper, ProductsContext db)
        {
            this.mapper = mapper;
            this.db = db;
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    db.Dispose();
                }
                disposedValue = true;
            }
        }
        public void Dispose()
        {
            Dispose(disposing: true);
            GC.SuppressFinalize(this);
        }
    }
}

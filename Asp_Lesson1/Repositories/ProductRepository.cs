using Asp_Lesson1.Abstractions;
using Asp_Lesson1.Models;
using Asp_Lesson1.Models.DTO;
using AutoMapper;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace Asp_Lesson1.Repositories
{
    public class ProductRepository : IRepository<ProductModel>
    {
        private readonly ProductsContext db;
        private readonly IMapper mapper;
        public ProductRepository(ProductsContext context, IMapper mapper) 
        {
            db = context;
            this.mapper = mapper;
        }
        public int Create(ProductModel product)
        {
            if (db.Products.Any(x => x.Name.ToLower() == product.Name.ToLower()))
            {
                throw new InvalidOperationException($"Продукт с именем {product.Name} уже существует"); 
            }
            else
            {
                Product productEntity = mapper.Map<Product>(product);
                db.Products.Add(productEntity);
                db.SaveChanges();
                return productEntity.Id;
            }
        }

        public void Delete(int id)
        {
            var product = db.Products.FirstOrDefault(x => x.Id == id);
            if (product == null)
            {
                throw new InvalidOperationException($"Не существует продукта с ID {id}");
            }
            else
            {
                db.Products.Remove(product);
                db.SaveChanges();
            }
        }

        public IEnumerable<ProductModel> GetAll()
        {
            
            var productList = db.Products.Select(x=> mapper.Map<ProductModel>(x)).ToList();
            return productList;
        }

        public void Update(ProductModel item)
        {
            var product = db.Products.FirstOrDefault(y => y.Id == item.Id);
            if (product == null)
            {
                throw new InvalidOperationException($"Не существует продукта с ID {item.Id}");
            }
            else
            {
                product.Price = item.Price;
                db.SaveChanges();
            }
        }
    }
}

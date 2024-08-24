using Asp_Lesson1.Abstractions;
using Asp_Lesson1.Models;
using Asp_Lesson1.Models.DTO;
using AutoMapper;
using System.Xml.Linq;

namespace Asp_Lesson1.Repositories
{
    public class ProductGroupRepository : IRepository<ProductGroupModel>
    {
        private readonly ProductsContext db;
        private readonly IMapper mapper;
        public ProductGroupRepository(ProductsContext productsContext, IMapper mapper) 
        {
            db = productsContext;
            this.mapper = mapper;
        }

        public int Create(ProductGroupModel productGroup)
        {
            if (db.ProductGroups.Any(x => x.Name.ToLower() == productGroup.Name.ToLower()))
            {
                throw new InvalidOperationException($"Группа с именем {productGroup.Name} уже существует");
            }
            else
            {
                var productGroupEntity = mapper.Map<ProductGroup>(productGroup);
                db.ProductGroups.Add(productGroupEntity);
                db.SaveChanges();
                return productGroupEntity.Id;
            }
        }

        public void Delete(int id)
        {
            var productGroup = db.ProductGroups.FirstOrDefault(x => x.Id == id);
            if (productGroup == null)
            {
                throw new InvalidOperationException($"Группа с именем {productGroup.Name} уже существует");
            }
            else
            {
                db.ProductGroups.Remove(productGroup);
                db.SaveChanges();
            }
        }

        public IEnumerable<ProductGroupModel> GetAll()
        {
            var groupList = db.ProductGroups.Select(x=> mapper.Map<ProductGroupModel>(x)).ToList();
            return groupList;
        }

        public void Update(ProductGroupModel item)
        {
            throw new NotImplementedException();
        }
    }
}

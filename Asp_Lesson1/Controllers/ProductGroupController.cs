using Asp_Lesson1.Models;
using Asp_Lesson1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Lesson1.Controllers
{
    public class ProductGroupController : ControllerBase
    {
        [HttpPost("addGroup")]
        public ActionResult AddGroup(string name, string? description = null)
        {
            try
            {
                using (ProductsContext productsContext = new ProductsContext())
                {
                    if (productsContext.ProductGroups.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        ProductGroup productGroup = new ProductGroup() { Description = description, Name = name };
                        productsContext.ProductGroups.Add(productGroup);
                        productsContext.SaveChanges();
                    }
                }
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpGet("getGroup")]
        public ActionResult<IEnumerable<ProductGroup>> GetGroups()
        {
            try
            {
                using (ProductsContext productsContext = new ProductsContext())
                {
                    var list = productsContext.ProductGroups.ToList();
                    return list;
                }
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpDelete("deleteGroup")]
        public ActionResult Delete(int id)
        {
            try
            {
                using (ProductsContext productsContext = new ProductsContext())
                {
                    ProductGroup? group = productsContext.ProductGroups.FirstOrDefault(x => x.Id == id);
                    if (group != null)
                    {
                        productsContext.ProductGroups.Remove(group);
                        productsContext.SaveChanges();
                    }
                    else
                    {
                        return StatusCode(409);
                    }
                }
                return Ok();
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}

using Asp_Lesson1.Models;
using Asp_Lesson1.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Asp_Lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpPost("addProduct")]
        public ActionResult AddProduct(string name, int groupId, double price, string? description = null) 
        {
            try
            {
                using (ProductsContext productsContext = new ProductsContext())
                {
                    if (productsContext.Products.Any(x => x.Name.ToLower() == name.ToLower()))
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        Product product = new Product() { Description = description, ProductGroupId = groupId, Price = price, Name = name };
                        productsContext.Products.Add(product);
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

        
        [HttpGet("getProduct")]
        public ActionResult<IEnumerable<Product>> GetProducts()
        {
            try
            {
                using(var productsContext = new ProductsContext())
                {
                    var list = productsContext.Products.ToList();
                    return list;
                }
            }
            catch 
            { 
                return StatusCode(500); 
            }
        }
        [HttpDelete("deleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                using(ProductsContext productsContext = new ProductsContext())
                {
                    var product = productsContext.Products.FirstOrDefault(x => x.Id == id);
                    if (product == null)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        productsContext.Products.Remove(product);
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
        [HttpPatch("SetPrice")]
        public ActionResult SetNewPrice(int id, int price)
        {
            try
            {
                using(ProductsContext productContext = new ProductsContext())
                {
                    var product = productContext.Products.FirstOrDefault(y => y.Id == id);
                    if(product == null)
                    {
                        return StatusCode(409);
                    }
                    else
                    {
                        product.Price = price;
                        productContext.SaveChanges();
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

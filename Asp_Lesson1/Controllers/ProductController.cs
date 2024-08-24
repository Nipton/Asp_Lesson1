using Asp_Lesson1.Abstractions;
using Asp_Lesson1.Models;
using Asp_Lesson1.Models.DTO;
using Asp_Lesson1.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Text;

namespace Asp_Lesson1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork unitOfWork;
        private IMemoryCache cache;
        public ProductController(IUnitOfWork unitOfWork, IMemoryCache memoryCache) 
        {
            this.unitOfWork = unitOfWork;
            cache = memoryCache;
        }
        [HttpPost("addProduct")]
        public ActionResult AddProduct(string name, int groupId, double price, string? description = null) 
        {
            try
            {
                ProductModel productModel = new ProductModel() { Description = description, ProductGroupId = groupId, Price = price, Name = name }; ;
                unitOfWork.Products.Create(productModel);
                unitOfWork.Dispose();
                cache.Remove("products");
                return Ok();
            }
            catch(InvalidOperationException ex)
            {
                return StatusCode(409);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        
        [HttpGet("getProduct")]
        public ActionResult<IEnumerable<ProductModel>> GetProducts()
        {
            if (cache.TryGetValue("prod", out List<ProductModel> cachedProducts))
            {
                return cachedProducts;
            }
            else
            {
                try
                {
                    var products = unitOfWork.Products.GetAll().ToList();
                    cache.Set("prod", products, TimeSpan.FromMinutes(30));
                    unitOfWork.Dispose();
                    return Ok(products);
                }
                catch
                {
                    return StatusCode(500);
                }
            }
        }
        [HttpDelete("deleteProduct")]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                unitOfWork.Products.Delete(id);
                unitOfWork.Dispose();
                cache.Remove("products");
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(409);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        [HttpPatch("SetPrice")]
        public ActionResult SetNewPrice(int id, int price)
        {
            ProductModel productModel = new ProductModel() { Id = id, Price = price};
            try
            {
                unitOfWork.Products.Update(productModel);
                unitOfWork.Dispose();
                cache.Remove("products");
                return Ok();
            }
            catch (InvalidOperationException ex)
            {
                return StatusCode(409);
            }
            catch
            {
                return StatusCode(500);
            }
        }
        private string GetCsv(IEnumerable<ProductModel> products, IEnumerable<ProductGroupModel> groupProducts)
        {
            StringBuilder sb = new StringBuilder();
            foreach (var product in products)
            {
                sb.AppendLine(product.Name + ";" + product.Price + ";" + groupProducts.First(x=> x.Id == product.ProductGroupId).Name + ";" + product.Description + "\n");
            }
            return sb.ToString();
        }
        [HttpGet(template: "GetProductCSV")]
        public FileContentResult GetProductCsv()
        {
            var content = "";
            var product = unitOfWork.Products.GetAll();
            var groupProduct = unitOfWork.GroupProduct.GetAll();
            unitOfWork.Dispose();
            content = GetCsv(product, groupProduct);
            return File(new UTF8Encoding().GetBytes(content), "text/csv", "report.csv");
        }
        [HttpGet(template: "GetCacheStats")]
        public ActionResult<string> GetProductCsvUrl()
        {
            var content = GetCacheStats();
            string fileName = "CacheStatictics" + ".txt";
            System.IO.File.WriteAllText(Path.Combine(Directory.GetCurrentDirectory(), "StaticFiles", fileName), content);
            return "https://" + Request.Host.ToString() + "/static" + fileName;
        }
        private string GetCacheStats()
        {
            var stats =  cache.GetCurrentStatistics()!;
            string currentEstimatedSize = "";
            if (stats.CurrentEstimatedSize == null)
                currentEstimatedSize = "null";
            else
                currentEstimatedSize = stats.CurrentEstimatedSize.ToString()!;
            string strStats = $"currentEntryCount: {stats.CurrentEntryCount},\ncurrentEstimatedSize: {currentEstimatedSize},\ntotalMisses: {stats.TotalMisses},\ntotalHits: {stats.TotalHits}";
            return strStats;
        }
    }
}

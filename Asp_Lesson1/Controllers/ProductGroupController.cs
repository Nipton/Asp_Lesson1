using Asp_Lesson1.Abstractions;
using Asp_Lesson1.Models;
using Asp_Lesson1.Models.DTO;
using Asp_Lesson1.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Asp_Lesson1.Controllers
{
    public class ProductGroupController : ControllerBase
    {
        IUnitOfWork unitOfWork;
        public ProductGroupController(IUnitOfWork unitOfWork) 
        {
            this.unitOfWork = unitOfWork;
        }
        [HttpPost("addGroup")]
        public ActionResult AddGroup(string name, string? description = null)
        {
            try
            {
                ProductGroupModel model = new ProductGroupModel() { Description = description, Name = name};
                unitOfWork.GroupProduct.Create(model);
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
        [HttpGet("getGroup")]
        public ActionResult<IEnumerable<ProductGroup>> GetGroups()
        {
            try
            {
                var groupList = unitOfWork.GroupProduct.GetAll();
                return Ok(groupList);
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
                unitOfWork.GroupProduct.Delete(id);
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
    }
}

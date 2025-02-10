using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ApiContext _context;
        public CategoriesController(ApiContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult CategoryList(){
            var categories = _context.Categories.ToList();
            return Ok(categories);
        }
        [HttpGet("GetCategoryById")] //GetCategoryById?id=1 -> şu şekilde de olabilir [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = _context.Categories.Find(id);
            if(category == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            return Ok(category);
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return Ok("Kategori ekleme işlemi başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteCategory(int id)
        {
            var category = _context.Categories.Find(id);
            if(category == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            _context.Categories.Remove(category);
            _context.SaveChanges();
            return Ok("Kategori silme işlemi başarılı");
        }
        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            var updatedCategory = _context.Categories.Find(category.CategoryId);
            if(updatedCategory == null)
            {
                return NotFound("Kategori bulunamadı");
            }
            updatedCategory.CategoryName = category.CategoryName;
            _context.SaveChanges();
            return Ok("Kategori güncelleme işlemi başarılı");
        }
    }
}
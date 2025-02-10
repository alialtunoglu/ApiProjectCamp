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
    public class ChefsController : ControllerBase
    {
        private readonly ApiContext _context;

        public ChefsController(ApiContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult ChefList()
        {
            var chefs = _context.Chefs.ToList();
            return Ok(chefs);
        }
        [HttpPost]
        public IActionResult CreateChef(Chef chef)
        {
            _context.Chefs.Add(chef);
            _context.SaveChanges();
            return Ok("Şef ekleme işlemi başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteChef(int id)
        {
            var chef = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound("Şef bulunamadı");
            }
            _context.Chefs.Remove(chef);
            _context.SaveChanges();
            return Ok("Şef silme işlemi başarılı");
        }
        [HttpPut]
        public IActionResult UpdateChef(Chef chef)
        {
            var updatedChef = _context.Chefs.Find(chef.ChefId);
            if (updatedChef == null)
            {
                return NotFound("Şef bulunamadı");
            }
            updatedChef.NameSurname = chef.NameSurname;
            updatedChef.Description = chef.Description;
            updatedChef.Title = chef.Title;
            _context.Chefs.Update(updatedChef);
            _context.SaveChanges();
            return Ok("Şef güncelleme işlemi başarılı");
        }
        [HttpGet("GetChefById")]
        public IActionResult GetChefById(int id)
        {
            var chef = _context.Chefs.Find(id);
            if (chef == null)
            {
                return NotFound("Şef bulunamadı");
            }
            return Ok(chef);
        }
    }
}
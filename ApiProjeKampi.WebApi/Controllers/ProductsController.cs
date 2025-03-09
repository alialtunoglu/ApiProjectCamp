using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IValidator<Product> _productValidator;
        private readonly ApiContext _context;
        private readonly IMapper _mapper;
        public ProductsController(IValidator<Product> productValidator, ApiContext context, IMapper mapper)
        {
            _productValidator = productValidator;
            _context = context;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            var products = _context.Products.ToList();
            return Ok(products);
        }
        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var validationResult = _productValidator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            else 
            {
                _context.Products.Add(product);
                _context.SaveChanges();
                return Ok( "Ekleme İşlemi Başarılı");
            }
        }
        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var validationResult = _productValidator.Validate(product);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors.Select(x => x.ErrorMessage).ToList());
            }
            else
            {
                _context.Products.Update(product);
                _context.SaveChanges();
                return Ok("Güncelleme İşlemi Başarılı");
            }
        }
        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }
            _context.Products.Remove(product);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı");
        }
        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                return NotFound("Ürün Bulunamadı");
            }
            return Ok(_mapper.Map<GetByIdProductDto>(product));
        }

    }
}
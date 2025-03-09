using System;
using System.Collections.Generic;
using System.Linq;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.Services.ProductServices;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public IActionResult ProductList()
        {
            try
            {
                var products = _productService.GetAllProduct();
                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult CreateProduct(CreateProductDto createProductDto)
        {
            try
            {
                _productService.CreateProduct(createProductDto);
                return Ok("Ekleme İşlemi Başarılı");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        public IActionResult UpdateProduct(UpdateProductDto updateProductDto)
        {
            try
            {
                _productService.UpdateProduct(updateProductDto);
                return Ok("Güncelleme İşlemi Başarılı");
            }
            catch (ValidationException ex)
            {
                return BadRequest(ex.Errors.Select(x => x.ErrorMessage).ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        public IActionResult DeleteProduct(int id)
        {
            try
            {
                _productService.DeleteProduct(id);
                return Ok("Silme İşlemi Başarılı");
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetProduct")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var product = _productService.GetByIdProduct(id);
                return Ok(product);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
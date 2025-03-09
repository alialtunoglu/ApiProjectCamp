using System;
using System.Collections.Generic;
using System.Linq;
using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using FluentValidation;

namespace ApiProjeKampi.WebApi.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IValidator<Product> _productValidator;
        private readonly ApiContext _context;
        private readonly IMapper _mapper;

        public ProductService(IValidator<Product> productValidator, ApiContext context, IMapper mapper)
        {
            _productValidator = productValidator;
            _context = context;
            _mapper = mapper;
        }

        public void CreateProduct(CreateProductDto createProductDto)
        {
            var product = _mapper.Map<Product>(createProductDto);
            var validationResult = _productValidator.Validate(product);
            
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void DeleteProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Ürün Bulunamadı");
            }
            
            _context.Products.Remove(product);
            _context.SaveChanges();
        }

        public List<ResultProductDto> GetAllProduct()
        {
            var products = _context.Products.ToList();
            return _mapper.Map<List<ResultProductDto>>(products);
        }

        public GetByIdProductDto GetByIdProduct(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException("Ürün Bulunamadı");
            }
            
            return _mapper.Map<GetByIdProductDto>(product);
        }

        public void UpdateProduct(UpdateProductDto updateProductDto)
        {
            var product = _mapper.Map<Product>(updateProductDto);
            var validationResult = _productValidator.Validate(product);
            
            if (!validationResult.IsValid)
            {
                throw new ValidationException(validationResult.Errors);
            }
            
            _context.Products.Update(product);
            _context.SaveChanges();
        }
    }
}
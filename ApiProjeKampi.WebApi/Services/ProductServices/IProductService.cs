using System;
using System.Collections.Generic;
using System.Linq;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;

namespace ApiProjeKampi.WebApi.Services.ProductServices
{
    public interface IProductService
    {
        List<ResultProductDto> GetAllProduct();
        void CreateProduct(CreateProductDto createProductDto);
        void UpdateProduct(UpdateProductDto updateProductDto);
        void DeleteProduct(int id);
        GetByIdProductDto GetByIdProduct(int id);
    }
}
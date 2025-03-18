using Xunit;
using Moq;
using ApiProjeKampi.WebApi.Controllers;
using ApiProjeKampi.WebApi.Services.ProductServices;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ApiProjeKampi.WebApi.Entities;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;
using System.ComponentModel;

namespace ApiProjectCamp.Tests
{
    [Trait("Category", "Products API")]
    public class ProductsControllerTests
    {
        private readonly Mock<IProductService> _mockService;
        private readonly ProductsController _controller;
        private readonly List<ResultProductDto> _testProducts;
        private readonly GetByIdProductDto _testGetByIdProduct;

        public ProductsControllerTests()
        {
            _mockService = new Mock<IProductService>();
            _controller = new ProductsController(_mockService.Object);
            
            _testProducts = new List<ResultProductDto>
            {
                new ResultProductDto 
                { 
                    ProductId = 1, 
                    ProductName = "Test Product 1", 
                    Price = 100,
                    ProductDescription = "Test Description 1",
                    ImageUrl = "test1.jpg"
                },
                new ResultProductDto 
                { 
                    ProductId = 2, 
                    ProductName = "Test Product 2", 
                    Price = 200,
                    ProductDescription = "Test Description 2",
                    ImageUrl = "test2.jpg"
                }
            };

            _testGetByIdProduct = new GetByIdProductDto
            {
                ProductId = 1,
                ProductName = "Test Product 1",
                Price = 100,
                ProductDescription = "Test Description 1",
                ImageUrl = "test1.jpg"
            };
        }

        [Fact(DisplayName = "✓ LIST - Ürün listesi başarıyla getirilmeli")]
        public void ProductList_ShouldReturn_ListOfProducts_Successfully()
        {
            // Arrange
            _mockService.Setup(service => service.GetAllProduct())
                       .Returns(_testProducts);

            // Act
            var result = _controller.ProductList();

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var products = Assert.IsType<List<ResultProductDto>>(okResult.Value);
            Assert.Equal(_testProducts.Count, products.Count);
            Assert.Equal(_testProducts[0].ProductId, products[0].ProductId);
        }

        [Fact(DisplayName = "✓ GET - Tek ürün başarıyla getirilmeli")]
        public void GetProduct_ShouldReturn_SingleProduct_WhenIdExists()
        {
            // Arrange
            var productId = 1;
            _mockService.Setup(service => service.GetByIdProduct(productId))
                       .Returns(_testGetByIdProduct);

            // Act
            var result = _controller.GetProduct(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnedProduct = Assert.IsType<GetByIdProductDto>(okResult.Value);
            Assert.Equal(_testGetByIdProduct.ProductId, returnedProduct.ProductId);
        }

        [Fact(DisplayName = "✓ DELETE - Ürün başarıyla silinmeli")]
        public void DeleteProduct_ShouldReturn_Success_WhenIdExists()
        {
            // Arrange
            var productId = 1;
            _mockService.Setup(service => service.DeleteProduct(productId))
                       .Verifiable(); // Bu metod çağrılmalı

            // Act
            var result = _controller.DeleteProduct(productId);

            // Assert
            _mockService.Verify(service => service.DeleteProduct(productId), Times.Once());
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Silme İşlemi Başarılı", okResult.Value);
        }

        [Fact(DisplayName = "✗ DELETE - Olmayan ürün silinememeli")]
        public void DeleteProduct_ShouldReturn_NotFound_WhenIdDoesNotExist()
        {
            // Arrange
            var productId = 999;
            _mockService.Setup(service => service.DeleteProduct(productId))
                       .Throws(new KeyNotFoundException("Ürün bulunamadı"));

            // Act
            var result = _controller.DeleteProduct(productId);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal("Ürün bulunamadı", notFoundResult.Value);
        }

        [Fact(DisplayName = "✓ CREATE - Yeni ürün başarıyla eklenmeli")]
        public void CreateProduct_ShouldReturn_Success_WhenDataIsValid()
        {
            // Arrange
            var createDto = new CreateProductDto 
            { 
                ProductName = "Test Product",
                Price = 100,
                ProductDescription = "Test Description",
                ImageUrl = "test.jpg"
            };

            _mockService.Setup(service => service.CreateProduct(createDto))
                       .Verifiable();

            // Act
            var result = _controller.CreateProduct(createDto);

            // Assert
            _mockService.Verify(service => service.CreateProduct(createDto), Times.Once());
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Ekleme İşlemi Başarılı", okResult.Value);
        }

        [Fact(DisplayName = "✓ UPDATE - Ürün başarıyla güncellenmeli")]
        public void UpdateProduct_ShouldReturn_Success_WhenDataIsValid()
        {
            // Arrange
            var updateDto = new UpdateProductDto 
            { 
                ProductId = 1,
                ProductName = "Updated Product",
                Price = 150,
                ProductDescription = "Updated Description",
                ImageUrl = "updated.jpg"
            };

            _mockService.Setup(service => service.UpdateProduct(updateDto))
                       .Verifiable();

            // Act
            var result = _controller.UpdateProduct(updateDto);

            // Assert
            _mockService.Verify(service => service.UpdateProduct(updateDto), Times.Once());
            var okResult = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Güncelleme İşlemi Başarılı", okResult.Value);
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using ApiProjeKampi.WebApi.Services.ProductServices;
using ApiProjeKampi.WebApi.Dtos.ProductDtos;

namespace ApiProjeKampi.WebApi.Controllers
{
    public class ProductViewController : Controller
    {
        private readonly IProductService _productService;

        public ProductViewController(IProductService productService)
        {
            _productService = productService;
        }

        public IActionResult Index()
        {
            var products = _productService.GetAllProduct();
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateProductDto createProductDto)
        {
            try
            {
                _productService.CreateProduct(createProductDto);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Hata:", ex.Message);
                return View(createProductDto);
            }
        }
    }
}

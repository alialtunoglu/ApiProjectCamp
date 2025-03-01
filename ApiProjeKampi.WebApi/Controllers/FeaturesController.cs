using ApiProjeKampi.WebApi.Context;
using ApiProjeKampi.WebApi.Dtos.FeatureDtos;
using ApiProjeKampi.WebApi.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ApiProjeKampi.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ApiContext _context;
        public FeaturesController(IMapper mapper, ApiContext context)
        {
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public IActionResult FeatureList()
        {
            var values = _context.Features.ToList(); 
            return Ok(_mapper.Map<List<ResultFeatureDto>>(values));
        }
        [HttpPost]
        public IActionResult CreateFeature(CreateFeatureDto createFeatureDto)
        {
            var feature = _mapper.Map<Feature>(createFeatureDto); // Buradaki yazılan ifade sayesinde value feature türünde oluyor AutoMapper ile 
            _context.Features.Add(feature);
            _context.SaveChanges();
            return Ok("Ekleme İşlemi Başarılı");
        }
        [HttpDelete]
        public IActionResult DeleteFeature(int id)
        {
            var feature = _context.Features.Find(id);
            if (feature == null)
            {
                return NotFound("Özellik Bulunamadı");
            }
            _context.Features.Remove(feature);
            _context.SaveChanges();
            return Ok("Silme İşlemi Başarılı");
        }
        [HttpGet("GetFeature")]
        public IActionResult GetFeature(int id)
        {
            var feature = _context.Features.Find(id);
            if (feature == null)
            {
                return NotFound("Özellik Bulunamadı");
            }
            Console.WriteLine($"Feature type: {feature.GetType()}");
            
            return Ok(_mapper.Map<GetByIdFeatureDto>(feature));
        }
        [HttpPut]
        public IActionResult UpdateFeature(UpdateFeatureDto updateFeatureDto)
        {
            var feature = _mapper.Map<Feature>(updateFeatureDto);
            _context.Features.Update(feature);
            _context.SaveChanges();
            return Ok("Güncelleme İşlemi Başarılı");
            
        }    
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;
using StokTakipOtomasyon.Repositories.Abstracts;

namespace StokTakipOtomasyon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryController(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }


        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }


        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            // Map DTO To Domain Model
            var categoryDomainModel = new Category { Name = addCategoryRequestDto.Name };

            categoryDomainModel = await _categoryRepository.CreateAsync(categoryDomainModel);

            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StokTakipOtomasyon.Exceptions;
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


        /// <summary>
        /// Get All Categories
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            // Get All Categories 
            var categories = await _categoryRepository.GetAllAsync();

            // Map Domain Models To DTOs
            return Ok(_mapper.Map<List<CategoryDto>>(categories));
        }


        /// <summary>
        /// Get Category By Id
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <returns></returns>
        /// <exception cref="CategoryNotFoundException"></exception>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
        {
            // Find the domain model with given ID
            var categoryDomainModel = await _categoryRepository.GetByIdAsync(id);

            // Check if exists
            if (categoryDomainModel is null)
            {
                throw new CategoryNotFoundException("Sorry, the category with given Id not found!");
            }

            // Map Domain Model Back To DTO
            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }


        /// <summary>
        /// Create New Category
        /// </summary>
        /// <param name="addCategoryRequestDto"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] AddCategoryRequestDto addCategoryRequestDto)
        {
            // Map DTO To Domain Model
            var categoryDomainModel = new Category { Name = addCategoryRequestDto.Name };

            categoryDomainModel = await _categoryRepository.CreateAsync(categoryDomainModel);

            // Map Domain Model Back To DTO
            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }


        /// <summary>
        /// Update Existing Category
        /// </summary>
        /// <param name="id">Category Id</param>
        /// <param name="updateCategoryRequestDto"></param>
        /// <returns></returns>
        /// <exception cref="CategoryNotFoundException"></exception>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] int id,
                                        [FromBody] UpdateCategoryRequestDto updateCategoryRequestDto)
        {
            // Map DTO to Domain Model
            var categoryDomainModel = new Category { Name = updateCategoryRequestDto.Name };

            // Update name field
            categoryDomainModel = await _categoryRepository.UpdateAsync(id, categoryDomainModel);

            if (categoryDomainModel is null)
            {
                throw new CategoryNotFoundException("Sorry, the category with given Id not found!");
            }

            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }


        /// <summary>
        /// Delete Category By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="CategoryNotFoundException"></exception>
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id)
        {
            // Find the domain model with given Id
            var categoryDomainModel = await _categoryRepository.DeleteByIdAsync(id);

            if (categoryDomainModel is null)
            {
                throw new CategoryNotFoundException("Sorry, the category with given Id not found!");
            }

            // Map Domain Model back to DTO
            return Ok(_mapper.Map<CategoryDto>(categoryDomainModel));
        }

    }
}

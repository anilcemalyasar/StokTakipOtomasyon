using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Data;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;
using StokTakipOtomasyon.Repositories.Abstracts;

namespace StokTakipOtomasyon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;

        public ProductController(IProductRepository productRepository, 
            DataContext dbContext,
            IMapper mapper
            )
        {
            _productRepository = productRepository;
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestDto request)
        {
            var product = await _productRepository.AddProductAsync(request);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }

        // GET PRODUCTS 
        // GET: /api/products/filterOn=Name&filterQuery=Machine&sortBy=Price&isAscending=True&pageNumber=1&pageSize=5
        [HttpGet]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=5)
        {
            // Get All Products From Database
            var products = await _productRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);
            // Map to DTOs
            return Ok(_mapper.Map<List<ProductDto>>(products));
        }

        [HttpGet("{id:int}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            // Get Domain Model from Database
            var product = await _productRepository.GetProductByIdAsync(id);
            if (product == null)
            {
                return NotFound("There is no product with given ID");
            }

            // Map Domain Model To DTO
            return Ok(_mapper.Map<ProductDto>(product));
        }

        
        [HttpPut("{id:int}")]
        public async Task<IActionResult> UpdateProduct([FromRoute] int id, [FromBody] UpdateProductRequestDto updateProductRequestDto)
        {
            // Map DTO to Domain Model
            var productDomainModel = _mapper.Map<Product>(updateProductRequestDto);

            // Check if region exists
            productDomainModel = await _productRepository.UpdateProductAsync(id, productDomainModel);

            if (productDomainModel == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProductDto>(productDomainModel));
        }


    }
}

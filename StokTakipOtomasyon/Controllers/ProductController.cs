using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StokTakipOtomasyon.Data;
using StokTakipOtomasyon.Exceptions;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;
using StokTakipOtomasyon.Repositories.Abstracts;
using System.Text.Json;

namespace StokTakipOtomasyon.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly DataContext _dbContext;
        private readonly IMapper _mapper;
        private readonly ILogger<ProductController> _logger;

        public ProductController(IProductRepository productRepository, 
            DataContext dbContext,
            IMapper mapper,
            ILogger<ProductController> logger)
        {
            _productRepository = productRepository;
            _dbContext = dbContext;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Create New Product
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <exception cref="ProductNotFoundException"></exception>
        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] AddProductRequestDto request)
        {
            var product = await _productRepository.AddProductAsync(request);

            if (product == null)
            {
                throw new ProductNotFoundException("Sorry, this product could not be created!");
            }

            // Map created domain model to DTO
            _logger.LogInformation($"Finished AddProduct request with the data: {JsonSerializer.Serialize(product)}");
            return Ok(_mapper.Map<ProductDto>(product));
        }

        // GET PRODUCTS 
        // GET: /api/products/filterOn=Name&filterQuery=Machine&sortBy=Price&isAscending=True&pageNumber=1&pageSize=5
        [HttpGet]
        // [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetAllProducts([FromQuery] string? filterOn, [FromQuery] string? filterQuery,
            [FromQuery] string? sortBy, [FromQuery] bool? isAscending, [FromQuery] int pageNumber=1, [FromQuery] int pageSize=5)
        {
            _logger.LogInformation("GetAll Action Method was invoked");

            // Get All Products From Database
            var products = await _productRepository.GetAllAsync(filterOn, filterQuery, sortBy, isAscending ?? true, pageNumber, pageSize);

            // Map to DTOs
            _logger.LogInformation($"Finished GetAllProducts request with the data: {JsonSerializer.Serialize(products)}");

            return Ok(_mapper.Map<List<ProductDto>>(products));
        }


        /// <summary>
        /// Get Product By Id
        /// </summary>
        /// <param name="id">Product Id</param>
        /// <returns></returns>
        /// <exception cref="ProductNotFoundException"></exception>
        [HttpGet("{id:int}")]
        [Authorize(Roles = "Reader")]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {

            // Logging that informs about method invoked
            _logger.LogInformation("GetById Action Method was invoked!");

            // Get Domain Model from Database
            var product = await _productRepository.GetProductByIdAsync(id);
            
            if (product == null)
            {
                throw new ProductNotFoundException("Product with given ID not found!");
            }

            // Map Domain Model To DTO

            _logger.LogInformation($"Finished GetById request with the data: {JsonSerializer.Serialize(product)}");

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
                throw new ProductNotFoundException("There is no product with given ID");
            }

            _logger.LogInformation($"Finished UpdateProduct request with the data: {JsonSerializer.Serialize(productDomainModel)}");

            // Map updated domain model back to DTO
            return Ok(_mapper.Map<ProductDto>(productDomainModel));
        }

        // Update Stock
        // PATCH: /api/Product/{id}?quantity=1
        [HttpPatch]
        [Route("{id:int}")]
        public async Task<IActionResult> UpdateStock([FromRoute] int id, [FromQuery] int quantity)
        {
            // Find domain model with given ID
            var productDomainModel = await _productRepository.UpdateStockAsync(id, quantity);

            if (productDomainModel is null)
            {
                throw new ProductNotFoundException("There is no product with given ID");
            }

            // Map Domain Model To DTO
            _logger.LogInformation($"Finished UpdateStock request with the data: {JsonSerializer.Serialize(productDomainModel)}");
            return Ok(_mapper.Map<ProductDto>(productDomainModel));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteProduct([FromRoute] int id)
        {
            // Logging
            _logger.LogInformation("DeleteProduct Action Method was invoked!");

            // Find the domain model with given id
            var product = await _productRepository.DeleteProductByIdAsync(id);

            if (product is null)
            {
                throw new ProductNotFoundException("There is not any product with given Id!");
            }

            _logger.LogInformation($"Finished DeleteProduct request with the data: {JsonSerializer.Serialize(product)}");

            // Map Domain Model to DTO
            return Ok(_mapper.Map<ProductDto>(product));
        }

    }
}

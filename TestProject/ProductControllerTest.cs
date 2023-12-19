using AutoMapper;
using Moq;
using StokTakipOtomasyon.Controllers;
using StokTakipOtomasyon.Data;
using StokTakipOtomasyon.Repositories.Abstracts;
using System.Net;
using StokTakipOtomasyon.Models.Domain;
using StokTakipOtomasyon.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace TestProject
{
    public class ProductControllerTest
    {
        private readonly Mock<IProductRepository> _productRepositoryMock;
        private readonly Mock<IMapper> _mapper;
        private readonly ProductController _controller;

        public ProductControllerTest()
        {
            _productRepositoryMock = new Mock<IProductRepository>();
            _mapper = new Mock<IMapper>();
            _controller = new ProductController(_productRepositoryMock.Object, null, _mapper.Object);
        }

        [Fact]
        public async void GetById_ReturnsOkResult_WhenProductExists()
        {
            // Arrange
            int productId = 1;

            // Mock the behavior of GetProductByIdAsync method in the repository
            _productRepositoryMock.Setup(repo => repo.GetProductByIdAsync(productId))
                    .ReturnsAsync(new Product()); /* mock the product object */

            // Mock the behavior of the mapper
            _mapper.Setup(mapper => mapper.Map<ProductDto>(It.IsAny<Product>))
                .Returns(new ProductDto()); /* mock the DTO object */

            // Act
            var result = await _controller.GetById(productId);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var productDto = Assert.IsType<ProductDto>(okResult.Value);
            Assert.NotNull(productDto);
        }
    }
}
using DesafioProgramacao.Application.Controllers;
using DesafioProgramacao.Application.Models;
using DesafioProgramacao.Domain.Dtos;
using DesafioProgramacao.Domain.Entities;
using DesafioProgramacao.Domain.Interfaces;
using DesafioProgramacao.Service.Validators;
using FluentAssertions;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace DesafioProgramacao.Tests
{
    public class ProductControllerTests
    {
        private readonly Mock<IBaseService<Product>> _mockRepo = new();
        private readonly Mock<ILogger<ProductController>> _mockLogger = new();

        public ProductControllerTests()
        {
        }

        [Fact]
        public async Task Get_AllProducts_Returns3Items()
        {
            _mockRepo.Setup(repo => repo.Get<ProductDto>()).ReturnsAsync(GetProductDtosTests());
            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Get();

            var providers = ((OkObjectResult)result).Value as IEnumerable<ProductDto>;

            result.Should().BeEquivalentTo(new OkResult());
            providers.Count().Should().Be(3);
        }

        [Fact]
        public async Task Get_GetByIdNotFound_ShouldReturnNotFound()
        {
            int id = 1;
            _mockRepo
                .Setup(repo => repo.GetById<ProductDto>(It.IsAny<int>()))
                .ReturnsAsync((ProductDto)null);
            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Get(id);

            result.Should().BeEquivalentTo(new NotFoundResult());
        }

        [Fact]
        public async Task Get_GetByIdLessThen1_ShouldReturnBadRequest()
        {
            int id = 0;
            _mockRepo
                .Setup(repo => repo.GetById<ProductDto>(It.IsAny<int>()))
                .ReturnsAsync((ProductDto)null);
            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Get(id);

            result.Should().BeEquivalentTo(new BadRequestResult());
        }

        [Fact]
        public async Task Get_GetByIdFound_ShouldReturnOk()
        {
            int id = 1;
            var productOutput = new ProductDto()
            {
                Id = 1,
                Status = true,
                Description = "Test",
                ProviderId = 1,
                ManufacturingDate = DateTime.Now,
                ValidationDate = DateTime.Now
            };

            _mockRepo
                .Setup(repo => repo.GetById<ProductDto>(It.IsAny<int>()))
                .ReturnsAsync(productOutput);
            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Get(id);

            result.Should().BeEquivalentTo(new OkObjectResult(productOutput));
        }

        [Fact]
        public async Task Create_ProductIsNull_ShouldReturnBadRequest()
        {
            ProductCreateModel product = null;

            _mockRepo
                .Setup(repo => repo.Add<ProductCreateModel, ProductDto, ProductValidator>(It.IsAny<ProductCreateModel>()))
                .ThrowsAsync(new ValidationException(""));

            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Create(product);

            result
                .Should()
                .BeEquivalentTo(new BadRequestResult());
        }

        [Fact]
        public async Task Create_ManufacturingDateNotLessThenValidationDate_ShouldReturnBadRequest()
        {
            var product = new ProductCreateModel();

            _mockRepo.Setup(x => x.Add<ProductCreateModel, ProductDto, ProductValidator>(It.IsAny<ProductCreateModel>()))
                .ThrowsAsync(new ValidationException(""));

            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Create(product);

            result
                .Should()
                .BeEquivalentTo(new BadRequestResult());
        }

        [Fact]
        public async Task Delete_IdLessThen1_ShouldReturnBadRequest()
        {
            var id = 0;

            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);
            var result = await productController.Delete(id);

            result.Should().BeEquivalentTo(new BadRequestResult());
        }

        [Fact]
        public async Task Delete_ProductNotFound_ShouldReturnBadRequest()
        {
            var id = 1;

            _mockRepo
                .Setup(repo => repo.Delete(
                    It.IsAny<int>(),
                    It.IsAny<bool>()
                    )).ReturnsAsync(false);
            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Delete(id);

            result.Should().BeEquivalentTo(new BadRequestResult());
        }

        [Fact]
        public async Task Delete_ProductFound_ShouldReturnOk()
        {
            var id = 1;

            _mockRepo
                .Setup(repo => repo.Delete(
                    It.IsAny<int>(),
                    It.IsAny<bool>()
                    )).ReturnsAsync(true);
            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Delete(id);

            result.Should().BeEquivalentTo(new OkResult());
        }

        [Fact]
        public async Task Update_ProductNotFound_ShouldReturnBadRequest()
        {
            var product = new ProductUpdateModel() { Id = 9999 };

            _mockRepo
                .Setup(repo => repo.Update<ProductUpdateModel, ProductDto, ProductValidator>(It.IsAny<ProductUpdateModel>()))
                .ThrowsAsync(new Exception());
            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Update(product);

            var statusCode = ((StatusCodeResult)result).StatusCode;

            statusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task Update_ProductFound_ShouldReturnOk()
        {
            var product = new ProductUpdateModel() { Id = 1 };
            var productDto = new ProductDto() { Id = 1 };

            _mockRepo
                .Setup(repo => repo.Update<ProductUpdateModel, ProductDto, ProductValidator>(It.IsAny<ProductUpdateModel>()))
                .ReturnsAsync(productDto);

            var productController = new ProductController(_mockRepo.Object, _mockLogger.Object);

            var result = await productController.Update(product);

            result.Should().BeEquivalentTo(new OkObjectResult(productDto));
        }

        private IEnumerable<ProductDto> GetProductDtosTests()
        {
            var providers = new List<ProductDto>();
            providers.Add(new ProductDto()
            {
                Id = 1,
                Status = true,
                Description = "Test",
                ProviderId = 1,
                ManufacturingDate = DateTime.Now,
                ValidationDate = DateTime.Now,
            });
            providers.Add(new ProductDto()
            {
                Id = 2,
                Status = true,
                Description = "Test",
                ProviderId = 1,
                ManufacturingDate = DateTime.Now,
                ValidationDate = DateTime.Now,
            });
            providers.Add(new ProductDto()
            {
                Id = 3,
                Status = true,
                Description = "Test",
                ProviderId = 1,
                ManufacturingDate = DateTime.Now,
                ValidationDate = DateTime.Now,
            });

            return providers;
        }
    }
}

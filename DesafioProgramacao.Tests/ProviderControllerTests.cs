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
    public class ProviderControllerTests
    {
        private readonly Mock<IBaseService<Provider>> _mockRepo = new();
        private readonly Mock<ILogger<ProviderController>> _mockLogger = new();        

        public ProviderControllerTests()
        {
        }

        [Fact]
        public async Task Get_AllProviders_Returns3Items()
        {
            _mockRepo.Setup(repo => repo.Get<ProviderDto>()).ReturnsAsync(GetProviderDtosTests());
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Get();

            var providers = ((OkObjectResult)result).Value as IEnumerable<ProviderDto>;            
            
            result.Should().BeEquivalentTo(new OkResult());
            providers.Count().Should().Be(3);
        }

        [Fact]
        public async Task Get_AllProviders_ShouldBeOkResult()
        {
            _mockRepo.Setup(repo => repo.Get<ProviderDto>()).ReturnsAsync(GetProviderDtosTests());
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Get();            

            result.Should().BeEquivalentTo(new OkResult());            
        }

        [Fact]
        public async Task Get_GetById_ShouldBeOkResult()
        {
            int testProviderId = 1;
            _mockRepo.Setup(repo => repo.GetById<ProviderDto>(testProviderId)).ReturnsAsync(GetProviderDtosTests().First());
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Get(1);

            result.Should().BeEquivalentTo(new OkResult());
        }

        [Fact]
        public async Task Get_GetById_ValuesShouldBeEqualsItem1()
        {
            int testProviderId = 1;
            _mockRepo.Setup(repo => repo.GetById<ProviderDto>(testProviderId)).ReturnsAsync(GetProviderDtosTests().First());
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Get(testProviderId);

            var provider = ((OkObjectResult)result).Value as ProviderDto;

            provider.Id.Should().Be(1);
            provider.Status.Should().Be(true);
            provider.Description.Should().Be("Test");
            provider.Cnpj.Should().Be("0005");            
        }

        [Fact]
        public async Task Get_GetById_ShouldReturnNotFound()
        {
            int testProviderId = 1;
            _mockRepo
                .Setup(repo => repo.GetById<ProviderDto>(testProviderId))
                .ReturnsAsync((ProviderDto)null);
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Get(testProviderId);            

            result.Should().BeEquivalentTo(new NotFoundResult());
        }

        [Fact]
        public async Task Post_CreateProvider_ShouldReturnProviderCreated()
        {
            var provider = new ProviderCreateModel() { Status = false, Description = "Test", Cnpj = "0000"};
            var providerOutput = new ProviderDto() { Id = 1, Status = false, Description = "Test", Cnpj = "0000" };

            _mockRepo
                .Setup(repo => repo.Add<ProviderCreateModel, ProviderDto, ProviderValidator>(It.IsAny<ProviderCreateModel>()))
                .ReturnsAsync(providerOutput);
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Create(provider);

            result.Should().BeEquivalentTo(new OkResult());
        }

        [Fact]
        public async Task Create_CreateProvider_ShouldReturnBadRequestCnpjInvalid()
        {
            var provider = new ProviderCreateModel() { Status = false, Description = "Test", Cnpj = "0000" };            

            _mockRepo
                .Setup(repo => repo.Add<ProviderCreateModel, ProviderDto, ProviderValidator>(It.IsAny<ProviderCreateModel>()))
                .ThrowsAsync(new ValidationException("Cnpj is not valid"));

            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Create(provider);            

            result.Should().BeEquivalentTo(new BadRequestResult());

            var exceptionMessage = ((BadRequestObjectResult)result).Value as string;

            exceptionMessage.Should().Be("Cnpj is not valid");
        }

        [Fact]
        public async Task Delete_IdLessThen1_ShouldReturnBadRequest()
        {
            var providerId = 0;

            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Delete(providerId);

            result.Should().BeEquivalentTo(new BadRequestResult());
        }

        [Fact]
        public async Task Delete_ProviderNotFound_ShouldReturnBadRequest()
        {
            var providerId = 1;

            _mockRepo
                .Setup(repo => repo.Delete(
                    It.IsAny<int>(), 
                    It.IsAny<bool>()
                    )).ReturnsAsync(false);
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Delete(providerId);

            result.Should().BeEquivalentTo(new BadRequestResult());
        }

        [Fact]
        public async Task Delete_ProviderFound_ShouldReturnOk()
        {
            var providerId = 1;

            _mockRepo
                .Setup(repo => repo.Delete(
                    It.IsAny<int>(),
                    It.IsAny<bool>()
                    )).ReturnsAsync(true);
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Delete(providerId);

            result.Should().BeEquivalentTo(new OkResult());
        }

        [Fact]
        public async Task Update_ProviderNotFound_ShouldReturnBadRequest()
        {            
            var provider = new ProviderUpdateModel() { Id = 9999, Description = "Test", Cnpj = "0000" };

            _mockRepo
                .Setup(repo => repo.Update<ProviderUpdateModel, ProviderDto, ProviderValidator>(It.IsAny<ProviderUpdateModel>()))
                .ThrowsAsync(new Exception());
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Update(provider);

            var statusCode = ((StatusCodeResult)result).StatusCode;

            statusCode.Should().Be(StatusCodes.Status500InternalServerError);
        }

        [Fact]
        public async Task Update_ProviderFoundCnpjInvalid_ShouldReturnBadRequest()
        {
            var provider = new ProviderUpdateModel() { Id = 1, Description = "Test", Cnpj = "0000" };

            _mockRepo
                .Setup(repo => repo.Update<ProviderUpdateModel, ProviderDto, ProviderValidator>(It.IsAny<ProviderUpdateModel>()))
                .ThrowsAsync(new ValidationException("Cnpj is not valid"));
            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Update(provider);

            result.Should().BeEquivalentTo(new BadRequestResult());

            var exceptionMessage = ((BadRequestObjectResult)result).Value as string;

            exceptionMessage.Should().Be("Cnpj is not valid");
        }

        [Fact]
        public async Task Update_ProviderFound_ShouldReturnOk()
        {
            var provider = new ProviderUpdateModel() { Id = 1, Description = "Test", Cnpj = "0000" };
            var providerDto = new ProviderDto() { Id = 1, Description = "Test", Cnpj = "0000", Status = true };

            _mockRepo
                .Setup(repo => repo.Update<ProviderUpdateModel, ProviderDto, ProviderValidator>(It.IsAny<ProviderUpdateModel>()))
                .ReturnsAsync(providerDto);

            var providerController = new ProviderController(_mockRepo.Object, _mockLogger.Object);

            var result = await providerController.Update(provider);

            result.Should().BeEquivalentTo(new OkObjectResult(providerDto));
        }


        private IEnumerable<ProviderDto> GetProviderDtosTests()
        {
            var providers = new List<ProviderDto>();
            providers.Add(new ProviderDto()
            {
                Id = 1,
                Status = true,
                Description = "Test",
                Cnpj = "0005"
            });
            providers.Add(new ProviderDto()
            {
                Id = 2,
                Status = true,
                Description = "Test",
                Cnpj = "0005"
            });
            providers.Add(new ProviderDto()
            {
                Id = 3,
                Status = true,
                Description = "Test",
                Cnpj = "0005"
            });

            return providers;
        }
    }


}

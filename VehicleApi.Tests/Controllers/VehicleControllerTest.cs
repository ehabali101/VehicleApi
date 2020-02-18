using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using VehicleApi.Mapping;
using VehicleResources;
using VehiclesApi.Controllers;
using VehiclesApi.Core;
using VehiclesApi.Core.Models;
using VehiclesApi.Core.Repositories;
using Xunit;

namespace VehicleApi.Tests.Controllers
{
    public class VehicleControllerTest
    {
        VehiclesController _controller;
        private Mock<IVehicleRepository> _mockVehicleRepository;
        private Mock<IVehicleOwnersRepository> _mockOwnerRepository;
        IMapper _mapper;

        public VehicleControllerTest()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new MappingProfile());
            });
            _mapper = config.CreateMapper();

            _mockVehicleRepository = new Mock<IVehicleRepository>();
            _mockOwnerRepository = new Mock<IVehicleOwnersRepository>();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(u => u.Vehicles).Returns(_mockVehicleRepository.Object);
            mockUnitOfWork.Setup(u => u.VehicleOwners).Returns(_mockOwnerRepository.Object);
            _controller = new VehiclesController(mockUnitOfWork.Object, _mapper);
        }

        [Fact]
        public async Task GetVehicles_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            _mockVehicleRepository.Setup(service => service.GetVehiclesAsync()).ReturnsAsync(
                new List<Vehicle>
                {
                    new Vehicle(),
                    new Vehicle()
                }
            );

            // Act
            var result = await _controller.GetVehicles();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetVehicle_WhenCalled_ReturnsOkResult()
        {

            // Arrange
            var id = "ab2bd817";
            _mockVehicleRepository.Setup(repo => repo.GetVehicleAsync(id))
                .ReturnsAsync((new Vehicle
                {
                    Id = "ab2bd817"
                }));

            // Act
            var result = await _controller.GetVehicle(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task PutVehicle_WhenCalled_ReturnsBadRequest()
        {
            // Arrange
            var id = "ab2bd817";
        
            // Act
            var result = await _controller.PutVehicle(id, new VehicleResource {  Id = id + "-" });

            // Assert
            Assert.NotNull(result);
            Assert.IsType<BadRequestResult>(result.Result);
        }

        [Fact]
        public async Task PutVehicle_WhenCalled_ReturnsNotFound()
        {
            // Arrange
            var id = "ab2bd817";
            _mockVehicleRepository.Setup(repo => repo.VehicleExists(id))
                .Returns(false);

            // Act
            var result = await _controller.PutVehicle(id, new VehicleResource { Id = id });
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<NotFoundResult>(result.Result);
        }

        [Fact]
        public async Task PutVehicle_WhenCalled_ReturnsOkResult()
        {
            // Arrange
            var id = "ab2bd817";
            _mockVehicleRepository.Setup(repo => repo.VehicleExists(id))
                .Returns(true);

            // Act
            var result = await _controller.PutVehicle(id, new VehicleResource { Id = id });
            
            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }

        [Fact]
        public async Task GetVehicleOwners_WhenCalled_ReturnsOkResult()
        {

            // Arrange
            _mockOwnerRepository.Setup(repo => repo.GetVehicleOwnersAsync())
                .ReturnsAsync((new List<VehicleOwners>
                {
                    new VehicleOwners(),
                    new VehicleOwners()
                }));

            // Act
            var result = await _controller.GetVehicleOwners();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<OkObjectResult>(result.Result);
        }
    }
}

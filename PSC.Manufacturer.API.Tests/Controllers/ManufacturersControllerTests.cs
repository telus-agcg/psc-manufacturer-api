using Microsoft.VisualStudio.TestPlatform.Common.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PSC.Manufacturer.API.Controllers;
using Microsoft.Extensions.Logging;
using PSC.Manufacturer.API.DataAccess;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

namespace PSC.Manufacturer.API.Tests.Controllers
{
    public class ManufacturersControllerTests
    {
        private readonly Mock<ILogger<ManufacturersController>> _logger;
        private readonly Mock<IManufacturerRepository> _repository;
        private readonly ManufacturersController _controller;

        public ManufacturersControllerTests()
        {
            _logger = new Mock<ILogger<ManufacturersController>>();
            _repository = new Mock<IManufacturerRepository>();
            _controller = new ManufacturersController(_repository.Object, _logger.Object);
        }

        [Fact]
        public async Task GetById_Succeeds()
        {
            var testEntity = new Core.Entities.Manufacturer()
            {
                Mfg_Key = 123,
                Address_1 = "test",
                Address_2 = "test",
                EMail_Address = "test",
                City = "test",
                State_Code = "test",
                Edi_Contact = "test",
                Fax = "test",
                Mfg_Name = "test",
                Mfg_Name_2 = "test",
                Mfg_RAPID_ICC = "test",
                Old_Mfg_Key = 111,
                Include_In_Dashboard_Filter = true,
                Inserted_Date_Time = new DateTime(2023, 02, 03),
                Phone = "test",
                Vendor_Key = 45,
                Zip_Code = "test"
            };

            _repository.Setup(x=>x.GetManufacturerById(It.IsAny<int>()))
                .ReturnsAsync(testEntity);

            var result = await _controller.Get(testEntity.Mfg_Key) as OkObjectResult;

            var resultObject = Assert.IsType<Core.Entities.Manufacturer>(result.Value);
            Assert.Equal(testEntity, resultObject);
            Assert.Equal(testEntity.Mfg_Key, resultObject.Mfg_Key);
            Assert.Equal(testEntity.Address_1, resultObject.Address_1);
            Assert.Equal(testEntity.Address_2, resultObject.Address_2);
            Assert.Equal(testEntity.Fax, resultObject.Fax);
            Assert.Equal(testEntity.Mfg_Name, resultObject.Mfg_Name);
            Assert.Equal(testEntity.Mfg_Name_2, resultObject.Mfg_Name_2);
            Assert.Equal(testEntity.Mfg_RAPID_ICC, resultObject.Mfg_RAPID_ICC);
            Assert.Equal(testEntity.Vendor_Key, resultObject.Vendor_Key);
            Assert.Equal(testEntity.City, resultObject.City);
            Assert.Equal(testEntity.Edi_Contact, resultObject.Edi_Contact);
            Assert.Equal(testEntity.EMail_Address, resultObject.EMail_Address);
            Assert.Equal(testEntity.Zip_Code, resultObject.Zip_Code);
            Assert.Equal(testEntity.State_Code, resultObject.State_Code);
            Assert.Equal(testEntity.Old_Mfg_Key, resultObject.Old_Mfg_Key);
            Assert.Equal(testEntity.Inserted_Date_Time, resultObject.Inserted_Date_Time);
            Assert.Equal(testEntity.Include_In_Dashboard_Filter, resultObject.Include_In_Dashboard_Filter);
            Assert.Equal(testEntity.Phone, resultObject.Phone);
        }

        [Fact]
        public async Task GetById_Errors()
        {
            var exception = new InvalidOperationException();
            _repository.Setup(x => x.GetManufacturerById(It.IsAny<int>())).Throws<InvalidOperationException>();
            var errorcode = StatusCodes.Status500InternalServerError;

            var result = await _controller.Get(0);

            var resultObject = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(errorcode, resultObject.StatusCode);
        }

        [Fact]
        public async Task GetById_NotFound()
        {
            _repository.Setup(x => x.GetManufacturerById(It.IsAny<int>()))
                .ReturnsAsync(new Core.Entities.Manufacturer());

            var result = await _controller.Get(0);

            var resultObject = Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async Task Create_Succeeds()
        {
            var testData = new Core.Entities.Manufacturer();
            var testKey = 123;

            _repository.Setup(x => x.Create(It.IsAny<Core.Entities.Manufacturer>()))
                .ReturnsAsync(testKey);

            var result = await _controller.Post(testData) as OkObjectResult;

            var resultObject = Assert.IsType<int>(result.Value);
            Assert.Equal(testKey, resultObject);
        }

        [Fact]
        public async Task Create_Errors()
        {
            _repository.Setup(x => x.Create(It.IsAny<Core.Entities.Manufacturer>()))
                .Throws<InvalidOperationException>();
            var errorcode = StatusCodes.Status500InternalServerError;

            var result = await _controller.Post(new Core.Entities.Manufacturer());

            var resultObject = Assert.IsType<StatusCodeResult>(result);
            Assert.Equal(errorcode, resultObject.StatusCode);
        }

        [Fact]
        public async Task Update_Succeeds()
        {
            var testToUpdate = new Core.Entities.Manufacturer()
            {
                Mfg_Key = 123,
                Address_1 = "test"
            };

            _repository.Setup(x => x.GetManufacturerById(It.IsAny<int>()))
    .ReturnsAsync(testToUpdate);

            var testData = new Core.Entities.Manufacturer()
            {
                Mfg_Key = 123
            };

            _repository.Setup(x => x.Update(It.IsAny<Core.Entities.Manufacturer>()))
                .ReturnsAsync("Updated");

            var result = await _controller.Put(testToUpdate.Mfg_Key, testData);
            var resultObject = Assert.IsType<OkObjectResult>(result);
            Assert.Equal("Updated", resultObject.Value);
        }

        [Fact]
        public async Task Update_Errors()
        {
            
        }

        [Fact]
        public async Task Update_NotFound()
        {

        }
    }
}

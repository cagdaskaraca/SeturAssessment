using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Data;
using SeturAssessment.Controllers;
using ContactService.Models;
using SeturAssessment.Services;
using Moq;
using System.Web.Http.Results;

namespace ContactControllerTest.Tests
{
    public class ContactControllerTests
    {
        private readonly ContactsController _controller;
        private readonly SeturAssessment.Services.ContactService _service;

        private readonly ContactDbContext _context;

        public ContactControllerTests()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString()) // her test için temiz db
                .Options;

            _context = new ContactDbContext(options);
            _context = new ContactDbContext(options);
            _service = new SeturAssessment.Services.ContactService(_context);
            _controller = new ContactsController(_service);
        }

        [Fact]
        public async Task GetAllContacts_ReturnsOkResult()
        {
            // Arrange
            await _service.CreateAsync(new ContactModel
            {
                Name = "Çağdaş",
                Surname = "Karaca",
                Email = "test@example.com",
                Company = "Setur"
            });

            // Act
            var result = await _controller.GetAll();

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var okResult = result as OkObjectResult;
            okResult.Value.Should().BeAssignableTo<IEnumerable<ContactModel>>();
            (okResult.Value as IEnumerable<ContactModel>).Should().ContainSingle();
        }

        [Fact]
        public async Task Create_ShouldReturnCreatedResult()
        {
            // Arrange
            var contactModel = new ContactModel
            {
                Id = Guid.NewGuid(),
                Name = "Test Name",
                Email = "test@example.com",
                Company = "Karaca Yazılım A.Ş"
            };

            var mockService = new Mock<IContactService>();
            mockService.Setup(s => s.CreateAsync(It.IsAny<ContactModel>())).ReturnsAsync(contactModel);

            var controller = new ContactsController(mockService.Object);

            // Act
            var result = await controller.Create(contactModel);

            // Assert
            var createdResult = Assert.IsType<CreatedAtActionResult>(result);
            var returnValue = Assert.IsType<ContactModel>(createdResult.Value);

            Assert.Equal(contactModel.Id, returnValue.Id);
            Assert.Equal(contactModel.Email, returnValue.Email);
        }

    }
}

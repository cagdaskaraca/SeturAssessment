using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SeturAssessment.Data;
using SeturAssessment.Controllers;
using ContactService.Models;
using SeturAssessment.Services;

namespace SeturAssessmentTest.Tests
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
    }
}

using SeturAssessment.Models;
using SeturAssessment.Services;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Moq;
using SeturAssessment.Data;
using System;
using System.Threading.Tasks;
using Xunit;
using ContactService.Models;

namespace ContactServiceTest.Tests
{
    public class ContactServiceTests
    {
        private ContactDbContext GetInMemoryDbContext()
        {
            var options = new DbContextOptionsBuilder<ContactDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var context = new ContactDbContext(options);
            return context;
        }

        [Fact]
        public async Task AddContact_ShouldAddContactSuccessfully()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new SeturAssessment.Services.ContactService(context);


            var newContact = new ContactModel
            {
                Name = "Çağdaş",
                Surname = "Karaca",
                Company = "Setur"
            };

            // Act
            await context.ContactModel.AddAsync(newContact);
            await context.SaveChangesAsync();

            // Assert
            context.ContactModel.Should().ContainSingle(c => c.Name == "Çağdaş" && c.Company == "Setur");
        }

        [Fact]
        public async Task GetContact_ShouldReturnCorrectContact()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new SeturAssessment.Services.ContactService(context);

            var contact = new ContactModel
            {
                Name = "Çağdaş",
                Surname = "Karaca",
                Company = "Setur"
            };

            await context.ContactModel.AddAsync(contact);
            await context.SaveChangesAsync();

            // Act
            var fetched = await context.ContactModel.FirstOrDefaultAsync(c => c.Name == "Çağdaş");

            // Assert
            fetched.Should().NotBeNull();
            fetched.Surname.Should().Be("Karaca");
        }

        [Fact]
        public async Task UpdateContact_ShouldUpdateSuccessfully()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new SeturAssessment.Services.ContactService(context);

            var contact = new ContactModel
            {
                Name = "Çağdaş",
                Surname = "Karaca",
                Company = "Setur"
            };

            await context.ContactModel.AddAsync(contact);
            await context.SaveChangesAsync();

            // Act
            contact.Company = "UpdatedSetur";
            context.ContactModel.Update(contact);
            await context.SaveChangesAsync();

            // Assert
            var updated = await context.ContactModel.FirstOrDefaultAsync(c => c.Name == "Çağdaş");
            updated.Company.Should().Be("UpdatedSetur");
        }

        [Fact]
        public async Task DeleteContact_ShouldRemoveContact()
        {
            // Arrange
            var context = GetInMemoryDbContext();
            var service = new SeturAssessment.Services.ContactService(context);

            var contact = new ContactModel
            {
                Name = "Çağdaş",
                Surname = "Karaca",
                Company = "Setur"
            };

            await context.ContactModel.AddAsync(contact);
            await context.SaveChangesAsync();

            // Act
            context.ContactModel.Remove(contact);
            await context.SaveChangesAsync();

            // Assert
            var exists = await context.ContactModel.AnyAsync(c => c.Name == "Çağdaş");
            exists.Should().BeFalse();
        }
    }
}

using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Data;
using PatientManagementAPI.Dto;
using PatientManagementAPI.Models;
using PatientManagementAPI.Repositories.Implementations;
using Xunit;

using RecordModel = PatientManagementAPI.Models.PatientRecord;

namespace PatientManagementAPI.Tests.Repositories
{
    public class RecordRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly RecordRepository _repository;

        public RecordRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            _context = new DataContext(options);
            _repository = new RecordRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllRecordsAsync_ReturnsRecords()
        {
            // Arrange
            var records = new List<RecordModel>
            {
                new RecordModel { Id = 1, Description = "Test Record 1", CardNumber = "12345", PatientId = 1, CreatedAt = DateTime.UtcNow },
                new RecordModel { Id = 2, Description = "Test Record 2", CardNumber = "67890", PatientId = 2, CreatedAt = DateTime.UtcNow }
            };

            await _context.PatientRecords.AddRangeAsync(records);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllRecordsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetRecordByIdAsync_ReturnsRecord_WhenFound()
        {
            // Arrange
            var record = new RecordModel { Id = 1, Description = "Test Record", CardNumber = "12345", PatientId = 1 };
            await _context.PatientRecords.AddAsync(record);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetRecordByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Record", result.Description);
        }

        [Fact]
        public async Task GetRecordByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Act
            var result = await _repository.GetRecordByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreateRecordAsync_CreatesAndReturnsRecord()
        {
            // Arrange
            var createDto = new CreateRecordDto { Description = "New Record", CardNumber = "54321", PatientId = 1 };
            var patient = new Patient { Id = 1, FirstName = "John Doe" };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.CreateRecordAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("New Record", result.Description);

            var savedRecord = await _context.PatientRecords.FindAsync(result.Id);
            Assert.NotNull(savedRecord);
        }

        [Fact]
        public async Task UpdateRecordAsync_UpdatesAndReturnsRecord_WhenFound()
        {
            // Arrange
            var record = new RecordModel { Id = 1, Description = "Old Description", CardNumber = "12345", PatientId = 1, CreatedAt = DateTime.UtcNow };
            await _context.PatientRecords.AddAsync(record);
            await _context.SaveChangesAsync();

            var updateDto = new UpdateRecordDto { Description = "Updated Description" };

            // Act
            var result = await _repository.UpdateRecordAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Updated Description", result.Description);

            var updatedRecord = await _context.PatientRecords.FindAsync(1);
            Assert.Equal("Updated Description", updatedRecord.Description);
        }
    }
}

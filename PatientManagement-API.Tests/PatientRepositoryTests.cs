using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Data;
using PatientManagementAPI.Dto;
using PatientManagementAPI.Models;
using PatientManagementAPI.Repositories.Implementations;
using Xunit;

using PatientModel = PatientManagementAPI.Models.Patient;

namespace PatientManagementAPI.Tests.Repositories
{
    public class PatientRepositoryTests : IDisposable
    {
        private readonly DataContext _context;
        private readonly PatientRepository _repository;

        public PatientRepositoryTests()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: "TestDb_Patient")
                .Options;

            _context = new DataContext(options);
            _repository = new PatientRepository(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }

        [Fact]
        public async Task GetAllPatientsAsync_ReturnsPatients()
        {
            // Arrange
            var patients = new List<PatientModel>
            {
                new PatientModel { Id = 1, FirstName = "John", LastName = "Doe" },
                new PatientModel { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            await _context.Patients.AddRangeAsync(patients);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetAllPatientsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsPatient_WhenFound()
        {
            // Arrange
            var patient = new PatientModel { Id = 1, FirstName = "John", LastName = "Doe" };
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            // Act
            var result = await _repository.GetPatientByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("John", result.FirstName);
        }

        [Fact]
        public async Task GetPatientByIdAsync_ReturnsNull_WhenNotFound()
        {
            // Act
            var result = await _repository.GetPatientByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task CreatePatientAsync_CreatesAndReturnsPatient()
        {
            // Arrange
            var createDto = new CreatePatientDto { FirstName = "Alice", LastName = "Johnson" };

            // Act
            var result = await _repository.RegisterPatientAsync(createDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Alice", result.FirstName);

            var savedPatient = await _context.Patients.FindAsync(result.Id);
            Assert.NotNull(savedPatient);
        }

        [Fact]
        public async Task UpdatePatientAsync_UpdatesAndReturnsPatient_WhenFound()
        {
            // Arrange
            var patient = new PatientModel { Id = 1, FirstName = "John", LastName = "Doe" };
            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            var updateDto = new UpdatePatientDto { FirstName = "UpdatedJohn", LastName = "Doe" };

            // Act
            var result = await _repository.UpdatePatientAsync(1, updateDto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("UpdatedJohn", result.FirstName);

            var updatedPatient = await _context.Patients.FindAsync(1);
            Assert.Equal("UpdatedJohn", updatedPatient.FirstName);
        }
    }
}

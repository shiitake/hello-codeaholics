using HelloCodeaholics.Data;
using HelloCodeaholics.Data.Entities;
using HelloCodeaholics.Services.Application;
using HelloCodeaholics.Services.Mapper;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.HelloCodeaholics.Services
{
    public class PharmacyServiceTests : IDisposable
    {
        private HelloCodeDbContext _context;
        private IGenericRepository<Pharmacy> _pharmacyRepository;

        public PharmacyServiceTests()
        {
            SetupContext();
            _pharmacyRepository = new DataRepository<Pharmacy>(_context);
        }

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
        public void SetupContext() 
        {
            var options = new DbContextOptionsBuilder<HelloCodeDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryPharmacyDatabase")
                .Options;

            _context = new HelloCodeDbContext(options);
            _context.Pharmacies.Add(new Pharmacy { Id = 1, Name = "Test Pharmacy", Address = "Test Address", City = "Test City", State = "Test State", Zip = 12345, FilledPrescriptionsCount = 10, CreatedDate = DateTime.Now, CreatedBy = "Test User" });
            _context.Pharmacies.Add(new Pharmacy { Id = 2, Name = "Test Pharmacy2", Address = "Test Address", City = "Test City", State = "Test State", Zip = 12345, FilledPrescriptionsCount = 10, CreatedDate = DateTime.Now, CreatedBy = "Test User" });
            _context.Pharmacies.Add(new Pharmacy { Id = 3, Name = "Test Pharmacy3", Address = "Test Address", City = "Test City", State = "Test State", Zip = 12345, FilledPrescriptionsCount = 10, CreatedDate = DateTime.Now, CreatedBy = "Test User" });
            _context.SaveChanges();
        }

        [Fact]
        public async Task GetPharmacyById_GivenId_ShouldReturnValidPharmacy()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var id = 1;

            // Act
            var result = await service.GetPharmacyById(id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(id, result.Id);
        }

        [Fact]
        public async Task GetPharmacyById_GivenNonExistentId_ShouldReturnNull()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var id = 9999; 

            // ACT
            var result = await service.GetPharmacyById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPharmacyById_GivenInvalidId_ShouldReturnNull()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var id = 0; 

            // Act
            var result = await service.GetPharmacyById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPharmacyById_GivenNegativeId_ShouldReturnNull()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var id = -1; 

            // Act
            var result = await service.GetPharmacyById(id);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task GetPharmacyList_ShouldReturnAllPharmacies()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);

            // Act
            var results = await service.GetPharmacyList();

            // Assert
            Assert.NotNull(results);
            Assert.Equal(3, results.Count);  
        }

        [Fact]
        public async Task GetPharmacyList_NoPharmaciesInDatabase_ShouldReturnEmptyList()
        {
            // Arrange
            var emptyContextOptions = new DbContextOptionsBuilder<HelloCodeDbContext>()
                .UseInMemoryDatabase(databaseName: "EmptyPharmacyDatabase")
                .Options;
            var emptyContext = new HelloCodeDbContext(emptyContextOptions); 
            var emptyRepository = new DataRepository<Pharmacy>(emptyContext);

            var service = new PharmacyService(emptyRepository);

            // Act
            var results = await service.GetPharmacyList();

            // Assert
            Assert.NotNull(results);
            Assert.Empty(results);
        }


        [Fact]
        public async Task PharmacyExists_GivenExistingId_ReturnsTrue()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var id = 1; // Id of existing pharmacy

            // Act
            var exists = await service.PharmacyExists(id);

            // Assert
            Assert.True(exists);
        }

        [Fact]
        public async Task PharmacyExists_GivenNonExistingId_ReturnsFalse()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var id = 9999; // Non-existing Id

            // Act
            var exists = await service.PharmacyExists(id);

            // Assert
            Assert.False(exists);
        }

        [Fact]
        public async Task UpdatePharmacy_GivenPharmacy_ShouldUpdateAndReturnUpdatedPharmacy()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var existingPharmacy = _context.Pharmacies.Find(1);

            var pharmacyModel = existingPharmacy!.Map();

            pharmacyModel!.Name = "Updated Name";

            // Act
            var updatedPharmacyModel = await service.UpdatePharmacy(pharmacyModel);
            var updatedPharmacy = _context.Pharmacies.First(x => x.Id == updatedPharmacyModel!.Id);

            // Assert
            Assert.Equal("Updated Name", updatedPharmacyModel!.Name);
            Assert.NotNull(updatedPharmacy.UpdatedDate);
        }

        [Fact]
        public async Task UpdatePharmacy_GivenInvalidPharmacy_ShouldReturnNull()
        {
            // Arrange
            var service = new PharmacyService(_pharmacyRepository);
            var invalidPharmacy = new Pharmacy() { Id = 7, Name = "Invalid Pharmacy" };
            var invalidModel = invalidPharmacy.Map();
            

            // Act
            var updatedPharmacy = await service.UpdatePharmacy(invalidModel);

            // Assert
            Assert.Null(updatedPharmacy);
        }

    }
}

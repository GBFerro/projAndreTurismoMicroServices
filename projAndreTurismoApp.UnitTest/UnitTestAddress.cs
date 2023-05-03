using System.Net;
using Microsoft.EntityFrameworkCore;
using projAndreTurismoApp.AddressService.Controllers;
using projAndreTurismoApp.AddressService.Data;
using projAndreTurismoApp.Models;
using projAndreTurismoApp.Services;

namespace projAndreTurismoApp.UnitTest
{
    public class UnitTestAddress
    {
        private DbContextOptions<projAndreTurismoAppAddressServiceContext> options;

        private void InitializeDataBase()
        {
            // Create a Temporary Database
            options = new DbContextOptionsBuilder<projAndreTurismoAppAddressServiceContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            // Insert data into the database using one instance of the context
            using (var context = new projAndreTurismoAppAddressServiceContext(options))
            {
                context.Address.Add(new Address { Id = 1, Street = "Street 1", ZipCode = "123456789", City = new City() { Id = 1, Name = "City1" } });
                context.Address.Add(new Address { Id = 2, Street = "Street 2", ZipCode = "987654321", City = new City() { Id = 2, Name = "City2" } });
                context.Address.Add(new Address { Id = 3, Street = "Street 3", ZipCode = "159647841", City = new City() { Id = 3, Name = "City3" } });
                context.SaveChanges();
            }
        }

        [Fact]
        public void GetAll()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new projAndreTurismoAppAddressServiceContext(options))
            {
                AddressesController clientController = new AddressesController(context, null);
                IEnumerable<Address> clients = clientController.GetAddress().Result.Value;

                Assert.Equal(3, clients.Count());
            }
        }

        [Fact]
        public void GetbyId()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new projAndreTurismoAppAddressServiceContext(options))
            {
                int clientId = 2;
                AddressesController clientController = new AddressesController(context, null);
                Address client = clientController.GetAddressById(clientId).Result.Value;
                Assert.Equal(2, client.Id);
            }
        }

        [Fact]
        public void Create()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 4,
                Street = "Rua 10",
                ZipCode = "14804300",
                City = new() { Id = 10, Name = "City 10" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new projAndreTurismoAppAddressServiceContext(options))
            {
                AddressesController clientController = new AddressesController(context, new PostOfficesService());
                Address ad = clientController.PostAddress(address).Result.Value;
                Assert.Equal("Avenida Alberto Benassi", ad.Street);
            }
        }

        [Fact]
        public void Update()
        {
            InitializeDataBase();

            Address address = new Address()
            {
                Id = 3,
                Street = "Rua 10 Alterada",
                City = new() { Id = 10, Name = "City 10 alterada" }
            };

            // Use a clean instance of the context to run the test
            using (var context = new projAndreTurismoAppAddressServiceContext(options))
            {
                AddressesController clientController = new AddressesController(context, null);
                clientController.PutAddress(3, address);
                Address ad = clientController.GetAddressById(address.Id).Result.Value;
                Assert.Equal("Rua 10 Alterada", ad.Street);
            }
        }

        [Fact]
        public void Delete()
        {
            InitializeDataBase();

            // Use a clean instance of the context to run the test
            using (var context = new projAndreTurismoAppAddressServiceContext(options))
            {
                AddressesController clientController = new AddressesController(context, null);
                clientController.DeleteAddress(3);
                Address ad = clientController.GetAddressById(3).Result.Value;
                Assert.Null(ad);
            }

        }
    }
}
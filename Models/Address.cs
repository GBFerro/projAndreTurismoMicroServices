using projAndreTurismoApp.Models.DTO;

namespace projAndreTurismoApp.Models
{
    public class Address
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public int Number { get; set; }
        public string? Neighborhood { get; set; }
        public string? ZipCode { get; set; }
        public string? Complement { get; set; }
        public City City { get; set; }
        public DateTime RegisterDate { get; set; }

        public Address() {}
        public Address(AddressDTO addressDTO, int number)
        {
            Street = addressDTO.Street;
            Number = number;
            Neighborhood = addressDTO.Neighborhood;
            ZipCode = addressDTO.ZipCode;
            RegisterDate = DateTime.Now;
            City = new City(addressDTO.City);
        }
    }
}

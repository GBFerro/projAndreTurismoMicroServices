namespace projAndreTurismoApp.Models
{
    public class Package
    {
        public int Id { get; set; }
        public Hotel Hotel { get; set; }
        public Ticket Ticket { get; set; }
        public Client Client { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}

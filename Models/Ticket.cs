namespace projAndreTurismoApp.Models
{
    public class Ticket
    {
        public int Id { get; set; }
        public Address Departure { get; set; }
        public Address Arrival { get; set; }
        //public Client Client { get; set; }
        public decimal Value { get; set; }
        public DateTime RegisterDate { get; set; }
    }
}

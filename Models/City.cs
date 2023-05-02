namespace projAndreTurismoApp.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegisterDate { get; set; }

        public City() {}
        public City(string name)
        {
            this.Name = name;
            RegisterDate = DateTime.Now;
        }
    }
}

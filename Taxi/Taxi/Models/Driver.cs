namespace Taxi
{
    public class Driver
    {
        public int Id { get; set; }
        public int Experience { get; set; }
        public User User { get; set; }
        public decimal Rating { get; set; }
    }
}
namespace Shread
{
    public class OrderCreatedEvent
    {
        public int Id { get; set; }
        public string OrderCode { get; set; } = null!;

        public int ProductCount { get; set; }

    }
}

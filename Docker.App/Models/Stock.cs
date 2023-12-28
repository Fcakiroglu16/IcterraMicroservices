namespace Docker.App.Models
{
    public class Stock
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public string Barcode { get; set; }
    }
}

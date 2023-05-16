namespace EBeats.Models
{
    public class Order
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public List<Product>? Products { get; set; }

        public float Total { get; set; }

    }
}

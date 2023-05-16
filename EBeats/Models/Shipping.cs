using System.ComponentModel.DataAnnotations.Schema;

namespace EBeats.Models
{

    public enum Status
    {
        Pending,
        InTransit,
        OutForDelivery,
        Delivered,
        Cancelled
    }
    public class Shipping
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public Status Status { get; set; }

        [ForeignKey("OrderId")]
        public int OrderId { get; set; }
        public Order ShippingOrder { get; set; }

        public string Address { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace EBeats.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }

        public string? Image { get; set; }
        public float Price { get; set; }

        public float Sale { get; set; }

        [ForeignKey("CategoryId")]
        public int? CategoryId { get; set; }
    }
}

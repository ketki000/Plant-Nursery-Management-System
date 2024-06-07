using System.ComponentModel.DataAnnotations;

namespace PlantNurseryAssessment.Models
{
    public enum SaleStatus
    {
        AVAILABLE,
        SOLD

    }
    public class Plant
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(50, MinimumLength = 1, ErrorMessage = "Name must be between 1 and 50 characters.")]
        public string Name { get; set; }
        [Required]
        [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number.")]
        public int Price { get; set; }
        
        public SaleStatus SaleStatus { get; set; } = SaleStatus.AVAILABLE;
        public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedOn { get; set; } = DateTime.Now.ToUniversalTime();
        //public int? CustomerId { get; set; }
        //public Customer? Customer { get; set; }
    }
}

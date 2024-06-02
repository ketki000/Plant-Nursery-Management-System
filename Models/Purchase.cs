using System.ComponentModel.DataAnnotations;

namespace PlantNurseryAssessment.Models
{
    public class Purchase
    {
        [Key]
        public int Id { get; set; }
        public int PlantId { get; set; }
        public Plant Plant { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();
        public DateTime UpdatedOn { get; set; } = DateTime.Now.ToUniversalTime();
    }
}

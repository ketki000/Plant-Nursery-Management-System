using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace PlantNurseryAssessment.Models
{
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]

        public string ? Name { get; set; } =  "string";
        [Phone]
        public string? PhoneNumber { get; set; } = "0000000000";
        public DateTime CreatedOn { get; set; } = DateTime.Now.ToUniversalTime();
       
    }
}

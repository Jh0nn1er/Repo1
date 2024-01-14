using System.ComponentModel.DataAnnotations;

namespace API_Forms.Models
{
    public class GetForm
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; }
        [Required, EmailAddress]
        public string Email { get; set; }
        [MaxLength(25)]
        public string? DocumentType { get; set; }
        [Required, MaxLength(15)]
        public string? Identifier { get; set; }
        [MaxLength(8000)]
        public string? Comment { get; set; }

        public DateTime? PaymentDate { get; set; }

    }
}

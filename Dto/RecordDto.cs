using System.ComponentModel.DataAnnotations;

namespace PatientManagementAPI.Dto
{
    public class RecordDto
    {
        public int Id { get; set; }

        [Required]
        public string? Description { get; set; } = string.Empty;

        [Required]
        public string? CardNumber { get; set; } = string.Empty;

        [Required]
        public int PatientId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreateRecordDto
    {
        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public string CardNumber { get; set; } = string.Empty;

        [Required]
        public int PatientId { get; set; }
    }

    public class UpdateRecordDto
    {
        [Required]
        public string? Description { get; set; } = string.Empty;
    }

}

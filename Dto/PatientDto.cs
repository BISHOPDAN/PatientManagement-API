using System;
using System.ComponentModel.DataAnnotations;

namespace PatientManagementAPI.Dto
{

    public class PatientDto
    {
        public int Id { get; set; }

        [Required, EmailAddress]
        public string? Email { get; set; } = string.Empty;

        [Required]
        public string? FirstName { get; set; } = string.Empty;

        [Required]
        public string? LastName { get; set; } = string.Empty;

        public string? PhoneNumber { get; set; } = string.Empty;
        public List<RecordDto> Records { get; set; } = new List<RecordDto>();
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

    public class CreatePatientDto
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        public string LastName { get; set; } = string.Empty;

        public string PhoneNumber { get; set; } = string.Empty;
    }

    public class UpdatePatientDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }

        [EmailAddress]
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
    }
}



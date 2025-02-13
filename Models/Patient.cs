using System.ComponentModel.DataAnnotations;

namespace PatientManagementAPI.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string FullName => $"{FirstName} {LastName}";
        public string? PhoneNumber { get; set; }
        public bool IsDeleted { get; set; } = false;
        public ICollection<PatientRecord> Records { get; set; } = new List<PatientRecord>();
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}

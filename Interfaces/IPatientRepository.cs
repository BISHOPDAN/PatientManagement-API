using PatientManagementAPI.Dto;
using PatientManagementAPI.Models;

namespace PatientManagementAPI.Interfaces
{
    public interface IPatientRepository
    {
        Task<IEnumerable<PatientDto>> GetAllPatientsAsync();
        Task<PatientDto?> GetPatientByIdAsync(int id);
        Task<Patient?> RegisterPatientAsync(CreatePatientDto dto);
        Task<bool> SoftDeletePatientAsync(int id);
        Task<PatientDto?> UpdatePatientAsync(int id, UpdatePatientDto dto);
    }
}

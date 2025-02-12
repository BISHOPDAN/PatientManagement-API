using PatientManagementAPI.Dto;
using PatientManagementAPI.Models;

namespace PatientManagementAPI.Repositories.Interfaces
{
    public interface IRecordRepository
    {
        Task<IEnumerable<RecordDto>> GetAllRecordsAsync();
        Task<RecordDto?> GetRecordByIdAsync(int id);
        Task<Record> CreateRecordAsync(CreateRecordDto dto);
        Task<RecordDto?> UpdateRecordAsync(int id, UpdateRecordDto dto);


    }
}


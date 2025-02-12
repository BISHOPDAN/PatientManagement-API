using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Data;
using PatientManagementAPI.Dto;
using PatientManagementAPI.Models;
using PatientManagementAPI.Repositories.Interfaces;

namespace PatientManagementAPI.Repositories.Implementations
{
    public class RecordRepository : IRecordRepository
    {
        private DataContext _context;
        public RecordRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<RecordDto>> GetAllRecordsAsync()
        {
            return await _context.Records
                .Select(r => new RecordDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    CardNumber = r.CardNumber,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt,
                    PatientId = r.PatientId
                })
                .ToListAsync();
        }



        public async Task<RecordDto?> GetRecordByIdAsync(int id)
        {
            var record =  await _context.Records
                .FindAsync(id);

            if (record == null)
                return null;

            return new RecordDto
            {
                Id = record.Id,
                Description = record.Description,
                CardNumber = record.CardNumber,
                CreatedAt = record.CreatedAt,
                UpdatedAt = record.UpdatedAt,
                PatientId = record.PatientId
            };
        }

        public async Task<Record> CreateRecordAsync(CreateRecordDto dto)
        {
            var patient = await _context.Patients
                .FirstOrDefaultAsync(p => p.Id == dto.PatientId);

            if (patient == null)
                throw new Exception("Patient not found.");

            var record = new Record
            {
                Description = dto.Description,
                PatientId = dto.PatientId,
                CardNumber = dto.CardNumber
            };

            await _context.Records.AddAsync(record);
            await _context.SaveChangesAsync();

            return record;
        }

        public async Task<RecordDto?> UpdateRecordAsync(int id, UpdateRecordDto dto)
        {
            var record = await _context.Records.FindAsync(id);
            if (record == null)
                return null;

            // 🔹 Update record fields
            record.Description = dto.Description;
            record.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return new RecordDto
            {
                Id = record.Id,
                Description = record.Description,
                CardNumber = record.CardNumber,
                PatientId = record.PatientId,
                CreatedAt = record.CreatedAt,
                UpdatedAt = record.UpdatedAt
            };
        }
    }
}


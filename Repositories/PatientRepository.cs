using Microsoft.EntityFrameworkCore;
using PatientManagementAPI.Models;
using PatientManagementAPI.Interfaces;
using PatientManagementAPI.Dto;
using PatientManagementAPI.Data;

namespace PatientManagementAPI.Repositories.Implementations
{
    public class PatientRepository : IPatientRepository
    {
        private DataContext _context;
        public PatientRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PatientDto>> GetAllPatientsAsync()
        {
            var patients = await _context.Patients
                .Where(p => !p.IsDeleted)
                .Include(p => p.Records)
                .ToListAsync();

            // ✅ Map to DTOs to exclude "patient" inside records
            return patients.Select(p => new PatientDto
            {
                Id = p.Id,
                FirstName = p.FirstName,
                LastName = p.LastName,
                Email = p.Email,
                PhoneNumber = p.PhoneNumber,
                CreatedAt = p.CreatedAt,
                UpdatedAt = p.UpdatedAt,
                Records = p.Records.Select(r => new RecordDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    CardNumber = r.CardNumber,
                    PatientId = r.PatientId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                }).ToList()
            }).ToList();
        }


        public async Task<PatientDto?> GetPatientByIdAsync(int id)
        {
            var patient = await _context.Patients
                .Where(p => !p.IsDeleted)
                .Include(p => p.Records)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (patient == null)
                return null;

            // ✅ Map to DTO to exclude the 'patient' property in records
            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt,
                Records = patient.Records.Select(r => new RecordDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    CardNumber = r.CardNumber,
                    PatientId = r.PatientId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                }).ToList()
            };
        }

        public async Task<Patient?> RegisterPatientAsync(CreatePatientDto dto)
        {

            var patient = new Patient
            {
                Email = dto.Email,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                PhoneNumber = dto.PhoneNumber
            };

            await _context.Patients.AddAsync(patient);
            await _context.SaveChangesAsync();

            return patient;
        }

        public async Task<PatientDto?> UpdatePatientAsync(int id, UpdatePatientDto dto)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return null;

            // ✅ Update only provided fields
            if (!string.IsNullOrEmpty(dto.FirstName)) patient.FirstName = dto.FirstName;
            if (!string.IsNullOrEmpty(dto.LastName)) patient.LastName = dto.LastName;
            if (!string.IsNullOrEmpty(dto.Email)) patient.Email = dto.Email;
            if (!string.IsNullOrEmpty(dto.PhoneNumber)) patient.PhoneNumber = dto.PhoneNumber;

            patient.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            // ✅ Return updated patient as DTO
            return new PatientDto
            {
                Id = patient.Id,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Email = patient.Email,
                PhoneNumber = patient.PhoneNumber,
                CreatedAt = patient.CreatedAt,
                UpdatedAt = patient.UpdatedAt,
                Records = patient.Records.Select(r => new RecordDto
                {
                    Id = r.Id,
                    Description = r.Description,
                    CardNumber = r.CardNumber,
                    PatientId = r.PatientId,
                    CreatedAt = r.CreatedAt,
                    UpdatedAt = r.UpdatedAt
                }).ToList()
            };
        }

        public async Task<bool> SoftDeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
                return false;

            patient.IsDeleted = true;
            patient.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }

    }
}


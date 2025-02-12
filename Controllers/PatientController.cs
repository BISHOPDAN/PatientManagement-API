using Microsoft.AspNetCore.Mvc;
using PatientManagementAPI.Interfaces;
using PatientManagementAPI.Dto;

namespace PatientManagementAPI.Controllers
{
    [Route("api/patient")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly IPatientRepository _patientRepository;

        public PatientController(IPatientRepository patientRepository)
        {
            _patientRepository = patientRepository;
        }

        // Get all patients
        [HttpGet("ListPatients")]
        public async Task<IActionResult> GetAllPatients()
        {
            var patients = await _patientRepository.GetAllPatientsAsync();
            return Ok(new
            {
                success = true,
                message = "Patients retrieved successfully.",
                data = patients
            }); ;
        }

        // get a patient by id
        [HttpGet("GetPatientById/{id}")]
        public async Task<IActionResult> GetPatientById(int id)
        {

            var patient = await _patientRepository.GetPatientByIdAsync(id);
            if (patient == null)
                return NotFound(new { message = "Patient not found." });

            return Ok(new
            {
                success = true,
                message = "Patient retrieved successfully.",
                data = patient
            }); ;
        }

        // create a patient
        [HttpPost("CreatePatient")]
        public async Task<IActionResult> RegisterPatient([FromBody] CreatePatientDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid input data." });
            try
            {
                var result = await _patientRepository.RegisterPatientAsync(dto);
                if (result == null)
                    return BadRequest(new { error = "failed to register a patient." });

                return Ok(new 
                { 
                    success = true, 
                    message = "Patient registered successfully.", 
                    data = dto 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        [HttpPut("UpdatePatient/{id}")]
        public async Task<IActionResult> UpdatePatient(int id, [FromBody] UpdatePatientDto dto)
        {
            var updatedPatient = await _patientRepository.UpdatePatientAsync(id, dto);
            if (updatedPatient == null)
                return NotFound(new { message = "Patient not found" });

            return Ok(new { message = "Patient updated successfully", data = updatedPatient });
        }

        [HttpDelete("SoftDeletePatient/{id}")]
        public async Task<IActionResult> SoftDeletePatient(int id)
        {
            var isDeleted = await _patientRepository.SoftDeletePatientAsync(id);
            if (!isDeleted)
                return NotFound(new { message = "Patient not found" });

            return Ok(new { message = "Patient deleted successfully (soft delete)" });
        }

    }
}

using Microsoft.AspNetCore.Mvc;
using PatientManagementAPI.Dto;
using PatientManagementAPI.Repositories.Interfaces;

namespace PatientManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordController : ControllerBase
    {
        private readonly IRecordRepository _recordRepository;

        public RecordController(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        // 🔹 Get all records
        [HttpGet("GetRecordList")]
        public async Task<IActionResult> GetAllRecords()
        {
            var records = await _recordRepository.GetAllRecordsAsync();
            return Ok(new
            {
                success = true,
                message = "Records retrieved successfully.",
                data = records
            });
        }

        // 🔹 Get a specific record by ID
        [HttpGet("GetRecordById/{id}")]
        public async Task<IActionResult> GetRecordById(int id)
        {
            var record = await _recordRepository.GetRecordByIdAsync(id);
            if (record == null)
                return NotFound("Record not found.");

            return Ok(new
            {
                success = true,
                message = "Record retrieved successfully.",
                data = record
            });
        }

        // 🔹 Create a new record
        [HttpPost("CreateRecord")]
        public async Task<ActionResult> CreateRecord([FromBody] CreateRecordDto dto)
        {
            if(!ModelState.IsValid)
                    return BadRequest(new { error = "Invalid input data." });
            try
            {
                var result = await _recordRepository.CreateRecordAsync(dto);

                if (result == null)
                    return BadRequest(new { error = "Failed to create record." });

                return Ok(new 
                { 
                    success = true, 
                    message = "Patient registered successfully.", 
                    data = dto 
                });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // 🔹 Update an existing record
        [HttpPut("UpdateRecord/{id}")]
        public async Task<IActionResult> UpdateRecord(int id, [FromBody] UpdateRecordDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new { error = "Invalid input data." });

            try
            {
                var updatedRecord = await _recordRepository.UpdateRecordAsync(id, dto);

                if (updatedRecord == null)
                    return NotFound(new { error = "Record not found or update failed." });

                return Ok(new
                {
                    success = true,
                    message = "Record updated successfully.",
                    data = updatedRecord
                });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}


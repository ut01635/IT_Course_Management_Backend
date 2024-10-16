using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Course_Management_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost]
        public async Task<ActionResult<Admin>> CreateAdmin([FromBody] Admin admin)
        {
            try
            {
                var createdAdmin = await _adminService.CreateAdminAsync(admin);
                return CreatedAtAction(nameof(GetAdminByNIC), new { nic = createdAdmin.NIC }, createdAdmin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Admin>>> GetAllAdmins()
        {
            try
            {
                var admins = await _adminService.RetrieveAllAdminsAsync();
                return Ok(admins);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpGet("{nic}")]
        public async Task<ActionResult<Admin>> GetAdminByNIC(string nic)
        {
            try
            {
                var admin = await _adminService.RetrieveAdminByNICAsync(nic);
                if (admin == null) return NotFound();
                return Ok(admin);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpPut("{nic}")]
        public async Task<IActionResult> UpdateAdmin(string nic, [FromBody] Admin admin)
        {
            if (nic != admin.NIC) return BadRequest();

            try
            {
                await _adminService.UpdateAdminAsync(admin);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        [HttpDelete("{nic}")]
        public async Task<IActionResult> DeleteAdmin(string nic)
        {
            try
            {
                await _adminService.RemoveAdminAsync(nic);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}

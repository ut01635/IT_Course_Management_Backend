﻿using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Course_Management_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;

        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateNotification([FromBody] Notification notification)
        {
            if (notification == null || string.IsNullOrEmpty(notification.Message) || string.IsNullOrEmpty(notification.StudentNIC))
            {
                return BadRequest("Invalid notification data.");
            }

            await _notificationService.AddNotificationAsync(notification);
            return CreatedAtAction(nameof(CreateNotification), notification);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotificationById(int id)
        {
            try
            {
                var notification = await _notificationService.GetNotificationByIdAsync(id);
                if (notification == null)
                {
                    return NotFound();
                }
                return Ok(notification);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}

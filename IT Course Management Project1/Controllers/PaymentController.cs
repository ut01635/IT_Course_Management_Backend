using IT_Course_Management_Project1.Entity;
using IT_Course_Management_Project1.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IT_Course_Management_Project1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPayments()
        {
            var payments = await _paymentService.GetAllPaymentsAsync();
            return Ok(payments);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPaymentById(int id)
        {
            var payment = await _paymentService.GetPaymentByIdAsync(id);
            if (payment == null) return NotFound();
            return Ok(payment);
        }

        [HttpPost]
        public async Task<IActionResult> AddPayment([FromBody] Payment payment)
        {
            if (payment == null) return BadRequest("Payment data is null.");
            var addedPayment = await _paymentService.AddPaymentAsync(payment);
            return CreatedAtAction(nameof(GetPaymentById), new { id = addedPayment.ID }, addedPayment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePayment(int id, [FromBody] Payment payment)
        {
            if (payment == null || payment.ID != id) return BadRequest("Payment data is invalid.");
            var updatedPayment = await _paymentService.UpdatePaymentAsync(payment);
            if (updatedPayment == null) return NotFound();
            return Ok(updatedPayment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePayment(int id)
        {
            var result = await _paymentService.DeletePaymentAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        [HttpGet("enrollment/{enrollmentId}")]
        public async Task<IActionResult> GetPaymentsByEnrollmentId(int enrollmentId)
        {
            var payments = await _paymentService.GetPaymentsByEnrollmentIdAsync(enrollmentId);
            return Ok(payments);
        }

        [HttpGet("nic/{nic}")]
        public async Task<IActionResult> GetPaymentsByNic(string nic)
        {
            var payments = await _paymentService.GetPaymentsByNicAsync(nic);
            if (payments == null || !payments.Any()) return NotFound();
            return Ok(payments);
        }
    }
}

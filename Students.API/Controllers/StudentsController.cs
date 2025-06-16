using Microsoft.AspNetCore.Mvc;
using StudentApplication.Interfaces;
using StudentApplication.Services;
using StudentDomain.Entities;

namespace Students.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsController : ControllerBase
    {
        private readonly IStudentService _service;

        public StudentsController(IStudentService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAllAsync());

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));       

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Student student, [FromQuery] List<int> subjectIds)
        {
            try
            {
                var result = await _service.CreateAsync(student, subjectIds);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] Student student)
            => Ok(await _service.UpdateAsync(id, student));

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }

        [HttpGet("classmates/{id}")]
        public async Task<IActionResult> GetClassmates(int id)
        {
            var student = await _service.GetByIdAsync(id);
            if (student == null) return NotFound();

            var classmates = await _service.GetClassmatesAsync(id);
            return Ok(classmates);
        }
    }
}

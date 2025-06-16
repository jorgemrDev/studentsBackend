using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentDTOs;
using StudentsInfrastructure.Data;

namespace Students.API.Controllers

{
    [ApiController]
    [Route("api/[controller]")]
    public class SubjectsController : ControllerBase
    {
        private readonly StudentDbContext _context;

        public SubjectsController(StudentDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var subjects = await _context.Subjects
            .Include(s => s.SubjectAssignments)
                .ThenInclude(sa => sa.Professor)
            .ToListAsync();

            var result = subjects.Select(s => new SubjectDTO
            {
                Id = s.Id,
                Name = s.Name,
                Credits = s.Credits,
                TeacherName = s.SubjectAssignments.FirstOrDefault()?.Professor?.FullName ?? "No asignado"
            });
            return Ok(result);
        }
    }
}

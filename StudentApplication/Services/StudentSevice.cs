using Microsoft.EntityFrameworkCore;
using StudentApplication.Interfaces;
using StudentDomain.Entities;
using StudentDTOs;
using StudentsInfrastructure.Data;

namespace StudentApplication.Services
{
    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _context;

        public StudentService(StudentDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Student>> GetAllAsync()
        {
            return await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Subject).ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _context.Students.Include(s => s.Enrollments).ThenInclude(e => e.Subject)
                                          .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<StudentDTO> CreateAsync(Student student, IEnumerable<int> subjectIds)
        {
            if (subjectIds.Count() != 3) throw new Exception("Debe seleccionar exactamente 3 materias");

            var subjects = await _context.Subjects.Include(s => s.SubjectAssignments)
                                                  .Where(s => subjectIds.Contains(s.Id)).ToListAsync();

            if (subjects.SelectMany(s => s.SubjectAssignments.Select(a => a.ProfessorId)).Distinct().Count() != 3)
                throw new Exception("No puede tener clases con el mismo profesor");

            student.Enrollments = subjectIds.Select(id => new Enrollment { SubjectId = id }).ToList();
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return new StudentDTO
            {
                Id = student.Id,
                FullName = $"{student.FirstName} {student.LastName}",
                DateOfBirth = student.DateOfBirth,
                Subjects = subjects.Select(s => s.Name).ToList()
            };
        }

        public async Task<Student> UpdateAsync(int id, Student updated)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) throw new Exception("Not found");

            student.FirstName = updated.FirstName;
            student.LastName = updated.LastName;
            student.Email = updated.Email;
            student.DateOfBirth = updated.DateOfBirth;
            await _context.SaveChangesAsync();

            return student;
        }

        public async Task DeleteAsync(int id)
        {
            var student = await _context.Students.FindAsync(id);
            if (student == null) return;

            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<StudentDTO>> GetClassmatesAsync(int studentId)
        {
            var viewerSubjects = await _context.Enrollments
                .Where(e => e.StudentId == studentId)
                .Select(e => e.SubjectId)
                .ToListAsync();

            var students = await _context.Students
                .Include(s => s.Enrollments)
                .ThenInclude(e => e.Subject)
                .ToListAsync();

            var result = students.Select(s =>
            {
                bool sharesSubject = s.Enrollments.Any(e => viewerSubjects.Contains(e.SubjectId));
                return new StudentDTO
                {
                    Id = s.Id,
                    FullName = sharesSubject ? $"{s.FirstName} {s.LastName}" : "******",
                    Subjects = s.Enrollments.Select(e => e.Subject.Name).ToList(),
                    Email = s.Email
                };
            }).ToList();

            return result;
        }
    }
}

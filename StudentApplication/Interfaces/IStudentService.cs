using StudentDomain.Entities;
using StudentDTOs;

namespace StudentApplication.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id);
        Task<StudentDTO> CreateAsync(Student student, IEnumerable<int> subjectIds);
        Task<Student> UpdateAsync(int id, Student student);
        Task DeleteAsync(int id);
        Task<IEnumerable<StudentDTO>> GetClassmatesAsync(int studentId);
    }
}

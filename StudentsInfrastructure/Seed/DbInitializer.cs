using StudentDomain.Entities;
using StudentsInfrastructure.Data;

namespace StudentInfrastructure.Seed
{
    public static class DbInitializer
    {
        public static void Seed(StudentDbContext context)
        {
            if (context.Subjects.Any() || context.Professors.Any())
                return; // Ya está inicializado

            // Crear materias
            var subjects = new List<Subject>
            {
                new() { Name = "Matemáticas" },
                new() { Name = "Física" },
                new() { Name = "Química" },
                new() { Name = "Lengua" },
                new() { Name = "Historia" },
                new() { Name = "Geografía" },
                new() { Name = "Biología" },
                new() { Name = "Arte" },
                new() { Name = "Inglés" },
                new() { Name = "Computación" }
            };
            context.Subjects.AddRange(subjects);
            context.SaveChanges();

            // Crear profesores
            var professors = new List<Professor>
            {
                new() { FullName = "Prof. García" },
                new() { FullName = "Prof. López" },
                new() { FullName = "Prof. Martínez" },
                new() { FullName = "Prof. Rodríguez" },
                new() { FullName = "Prof. Fernández" }
            };
            context.Professors.AddRange(professors);
            context.SaveChanges();

            // Asignar 2 materias a cada profesor
            var assignments = new List<SubjectAssignment>
            {
                new() { ProfessorId = professors[0].Id, SubjectId = subjects[0].Id },
                new() { ProfessorId = professors[0].Id, SubjectId = subjects[1].Id },

                new() { ProfessorId = professors[1].Id, SubjectId = subjects[2].Id },
                new() { ProfessorId = professors[1].Id, SubjectId = subjects[3].Id },

                new() { ProfessorId = professors[2].Id, SubjectId = subjects[4].Id },
                new() { ProfessorId = professors[2].Id, SubjectId = subjects[5].Id },

                new() { ProfessorId = professors[3].Id, SubjectId = subjects[6].Id },
                new() { ProfessorId = professors[3].Id, SubjectId = subjects[7].Id },

                new() { ProfessorId = professors[4].Id, SubjectId = subjects[8].Id },
                new() { ProfessorId = professors[4].Id, SubjectId = subjects[9].Id },
            };
            context.SubjectAssignments.AddRange(assignments);
            context.SaveChanges();
        }
    }
}
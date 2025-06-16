using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomain.Entities
{
    public class SubjectAssignment
    {
        public int Id { get; set; }
        public int ProfessorId { get; set; }
        public int SubjectId { get; set; }

        public Professor Professor { get; set; }
        public Subject Subject { get; set; }
    }
}

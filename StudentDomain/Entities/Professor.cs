﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomain.Entities
{
    public class Professor
    {
        public int Id { get; set; }
        public string FullName { get; set; }

        public ICollection<SubjectAssignment> SubjectAssignments { get; set; }
    }
}

﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentDomain.Entities
{
    public class Enrollment
    {
        public int Id { get; set; }
        public int StudentId { get; set; }
        public int SubjectId { get; set; }

        public Student Student { get; set; }
        public Subject Subject { get; set; }
    }
}

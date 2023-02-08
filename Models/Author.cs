﻿using System.Text.Json.Serialization;

namespace codeFirstApprochExample.Models
{
    public class Author
    { 
        public Author()
        {
            Courses = new HashSet<Courses>();
        }

        public int Id { get; set; } 
        public string Name { get; set; }
        public virtual ICollection<Courses> Courses { get; set; }
    }
}

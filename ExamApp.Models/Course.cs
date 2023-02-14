using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExamApp.Models
{
    public class Course
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
        public virtual Language Language { get; set; }
    }

    public record Language(Guid Id, string Title);
}

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Mailer.Models
{
    [Table("Course")]
    public class Course
    {
        public int ID { get; set; }

        [Required]
        [StringLength(255)]
        public string CourseName { get; set; }
        [Required]
        public DateTime StartDate { get; set; }
        [Required]
        public DateTime EndDate { get; set; }
    }
}
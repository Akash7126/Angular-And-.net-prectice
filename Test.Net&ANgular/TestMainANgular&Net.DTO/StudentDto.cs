using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMainANgular_Net.DTO
{
    public class StudentDto
    {
        [Key]
        public int Id { get; set; }
        public required string RegNo { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public required string Email { get; set; }
        public string DepartmentName { get; set; }
        // Foreign key to the Department
        public int? DepartmentId { get; set; }
        //public string? DepartmentName { get; set; }

    }
}

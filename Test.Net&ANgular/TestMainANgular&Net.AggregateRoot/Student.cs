using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMainANgular_Net.DTO;

namespace TestMainANgular_Net.AggregateRoot
{
    public class Student
    {
        public int Id { get; set; }
        public string RegNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }

        // Foreign key to the Department
        public int? DepartmentId { get; set; }


        // Navigation property to the Department
        public Department Department { get; set; }

        public void UpdateFromDto(StudentDto studentDto)
        {
            RegNo = studentDto.RegNo;
            FirstName = studentDto.FirstName;
            LastName = studentDto.LastName;
            DateOfBirth = studentDto.DateOfBirth;
            Email = studentDto.Email;
            DepartmentId = studentDto.DepartmentId;
           
        }

        public StudentDto ToDto()
        {
            return new StudentDto
            {
                Id = Id,
                RegNo = RegNo,
                FirstName = FirstName,
                LastName = LastName,
                DateOfBirth = DateOfBirth,
                Email = Email,
                DepartmentId = DepartmentId,
                DepartmentName = Department?.DepartmentShortName // Map DepartmentName if it exists
            };
        }
    }
}

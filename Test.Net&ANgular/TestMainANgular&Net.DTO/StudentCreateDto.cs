using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestMainANgular_Net.DTO
{
    public class StudentCreateDto
    {
        public string RegNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public int? DepartmentId { get; set; }
    }

}

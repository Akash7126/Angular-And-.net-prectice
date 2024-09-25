using System.Text.RegularExpressions;
using TestMainANgular_Net.DTO;

namespace TestMainANgular_Net.AggregateRoot
{
    public class Department
    {
        public int Id { get; set; }

        public string DepartmentFullName { get; set; }

        public string DepartmentShortName { get; set; }
        public ICollection<Student> Students { get; set; }

        public void MappedDepartmentFromDto(DepartmentDto departmentDto)
        {
            DepartmentFullName = departmentDto.DepartmentFullName;
            DepartmentShortName = departmentDto.DepartmentShortName;
        }

        public DepartmentDto DepartmentToDto()
        {
            return new DepartmentDto
            {
                Id = Id,
                DepartmentFullName =DepartmentFullName,
                DepartmentShortName = DepartmentShortName
            };
        }
    }
}

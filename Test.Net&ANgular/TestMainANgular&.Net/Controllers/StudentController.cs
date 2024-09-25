using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using TestMainANgular_Net.AggregateRoot.Validation;
using TestMainANgular_Net.DTO;
using TestMainANgular_Net.Handler;

namespace TestMainANgular_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class StudentController : Controller
    {
        private readonly StudentHandler _studentHandler;
        //private readonly DepartmentHandler _departmentHandler;
        private readonly StudentDtoValidator _studentDtoValidator;

        public StudentController(StudentHandler studentHandler, StudentDtoValidator studentDtoValidator)
        {
            _studentHandler = studentHandler;
            _studentDtoValidator = studentDtoValidator; 
        }


        [HttpGet("getAllStudent")]
        public async Task<IActionResult> GetAllStudents()
        {
            // Call the handler method and return the result
            return await _studentHandler.GetAllStudentAsync(); // Return IActionResult from the handler
        }

        

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] StudentDto studentDto)
        {
            // The handler method now handles validation and creation
            return await _studentHandler.CreateStudentAsync(studentDto);
        }

        

        

        [HttpPut("UpdateStudent")]
        public async Task<IActionResult> UpdateStudent(StudentDto studentDto)
        {
            return await _studentHandler.UpdateStudentAsync(studentDto);
        }


        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            return await _studentHandler.DeleteStudentAsync(id);
        }
    }
}

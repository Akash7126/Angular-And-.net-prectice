using Microsoft.AspNetCore.Mvc;
using TestMainANgular_Net.AggregateRoot.Validation;
using TestMainANgular_Net.DTO;
using TestMainANgular_Net.Handler;

namespace TestMainANgular_.Net.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : Controller
    {
        private readonly DepartmentHandler _departmentHandler;
        //private readonly DepartmentHandler _departmentHandler;
        private readonly DepartmentDtoValidator _departmentDtoValidator;

        public DepartmentController(DepartmentHandler departmentHandler, DepartmentDtoValidator departmentDtoValidator)
        {
            _departmentHandler = departmentHandler;
            _departmentDtoValidator = departmentDtoValidator;
        }


        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return Ok("Ready to create a Department");
        }

        [HttpPost]
        public async Task<IActionResult> Create(DepartmentDto departmentDto)
        {
            // The handler method now handles validation and creation
            return await _departmentHandler.CreateDepartmentAsync(departmentDto);
        }

        [HttpGet("GetAllDepartments")]
        public async Task<IActionResult> GetAllDepartments()
        {
            // Call the handler method and return the result
            return await _departmentHandler.GetAllDepartmentAsync(); // Return IActionResult from the handler
        }



        [HttpPost("UpdateDepartment")]
        public async Task<IActionResult> UpdateDepartment(DepartmentDto departmentDto)
        {
            return await _departmentHandler.UpdateDepartmentAsync(departmentDto);
        }


        [HttpPost("Delete")]
        public async Task<IActionResult> DeleteDepartment(int id)
        {
            return await _departmentHandler.DeleteDepartmentAsync(id);
        }
    }
}

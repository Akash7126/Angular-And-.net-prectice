using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using TestMainANgular_Net.AggregateRoot;
using TestMainANgular_Net.DTO;
using TestMainANgular_Net.DTO.Event;
using TestMainANgular_Net.Repository;

namespace TestMainANgular_Net.Handler
{
    public class StudentHandler
    {
        private readonly IRepository<Student> _repository;
        private readonly IRepository<Department> _departmentRepository;
        private readonly IValidator<StudentDto> _validator;

        public StudentHandler(IRepository<Student> repository , IRepository<Department> departmentRepository , IValidator<StudentDto> validator)
        {
            _repository = repository;
            _departmentRepository = departmentRepository;
            _validator = validator;
        }

        public async Task<IActionResult> CreateStudentAsync(StudentDto studentDto)
        {
            var validationResult = await _validator.ValidateAsync(studentDto);
            var response = new ResponseDto<StudentDto>();

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                return new BadRequestObjectResult(response);
            }

            var student = new Student();
            student.UpdateFromDto(studentDto);

            await _repository.AddAsync(student);
            response.Data = studentDto;
            response.IsSuccess = true;
            response.EventMessage = "Student ++++++++++++??? created successfully";

            return new OkObjectResult(response);
        }


        public async Task<IActionResult> GetAllStudentAsync()
        {
            var response = new ResponseDto<List<StudentDto>>();
            var students = await _repository.GetAllAsync();

            var studentDtos = students.Select(s => s.ToDto()).ToList();

            foreach (var studentDto in studentDtos)
            {
                if (studentDto.DepartmentId.HasValue)
                {
                    var department = await _departmentRepository.GetByIdAsync(studentDto.DepartmentId.Value);
                    if (department != null)
                    {
                        studentDto.DepartmentName = department.DepartmentShortName;
                    }
                }
            }

            response.Data = studentDtos;
            response.IsSuccess = true;
            response.EventMessage = "Students ++++++++++++++++++++ retrieved successfully";
            return new OkObjectResult(response);
        }


        public async Task<IActionResult> GetStudentByIdAsync(int id)
        {
            var student = await _repository.GetByIdAsync(id);
            return new OkObjectResult(student);
        }



        public async Task<IActionResult> UpdateStudentAsync(StudentDto studentDto)
        {
            var validationResult = await _validator.ValidateAsync(studentDto);
            var response = new ResponseDto<StudentDto>();

            if (!validationResult.IsValid)
            {
                response.IsSuccess = false;
                response.ErrorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
                return new BadRequestObjectResult(response);
            }

            var existingStudent = await _repository.GetByIdAsync(studentDto.Id);
            if (existingStudent == null)
            {
                return new NotFoundResult();
            }

            existingStudent.UpdateFromDto(studentDto);
            await _repository.UpdateAsync(existingStudent);

            response.Data = studentDto;
            response.IsSuccess = true;
            response.EventMessage = "Student ++++++++++++++++++++ updated successfully";
            return new OkObjectResult(response);
        }



        public async Task<IActionResult> DeleteStudentAsync(int id)
        {
            var response = new ResponseDto<string>();

            await _repository.DeleteAsync(id);

            response.Data = "Deleted successfully";
            response.IsSuccess = true;
            response.EventMessage = "Student deleted successfully";
            return new OkObjectResult(response);
        }


    }
}

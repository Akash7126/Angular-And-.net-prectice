using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMainANgular_Net.AggregateRoot;
using TestMainANgular_Net.DTO;
using TestMainANgular_Net.Repository;

namespace TestMainANgular_Net.Handler
{
    public class DepartmentHandler
    {
        private readonly IRepository<Department> _repository;
        
        private readonly IValidator<DepartmentDto> _validator;

        public DepartmentHandler(IRepository<Department> repository, IValidator<DepartmentDto> validator)
        {
            _repository = repository;
            _validator = validator;
        }



        public async Task<IActionResult> CreateDepartmentAsync(DepartmentDto departmentDto)
        {
            var validationResult = await _validator.ValidateAsync(departmentDto);

            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(new
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            var department = new Department();
            department.MappedDepartmentFromDto(departmentDto);

            await _repository.AddAsync(department);
            return new OkObjectResult(departmentDto);
        }

        public async Task<IActionResult> GetAllDepartmentAsync()
        {
            var departments = await _repository.GetAllAsync();

            // Fetch department details for each student
            var departmentDtos = departments.Select(s => s.DepartmentToDto()).ToList();

            return new OkObjectResult(departmentDtos);
        }

        //public async Task<IActionResult> GetDepartmentByIdAsync(DepartmentDto departmentDto)
        //{
        //    var student = await _repository.GetByIdAsync(departmentDto.Id);
        //    return new OkObjectResult(student);
        //}



        public async Task<IActionResult> UpdateDepartmentAsync(DepartmentDto departmentDto)
        {
            // Validate the DTO
            var validationResult = await _validator.ValidateAsync(departmentDto);
            if (!validationResult.IsValid)
            {
                return new BadRequestObjectResult(new
                {
                    Errors = validationResult.Errors.Select(e => e.ErrorMessage)
                });
            }

            // Fetch the existing student from the repository
            var existingStudent = await _repository.GetByIdAsync(departmentDto.Id);
            if (existingStudent == null)
            {
                return new NotFoundResult();
            }

            // Update the existing student entity from the DTO
            existingStudent.MappedDepartmentFromDto(departmentDto);

            // Save the changes to the repository
            await _repository.UpdateAsync(existingStudent);

            // Return a success response with the updated student DTO
            return new OkObjectResult(departmentDto);
        }


        public async Task<IActionResult> DeleteDepartmentAsync(int id)
        {
            await _repository.DeleteAsync(id);

            // Return a success response with the updated student DTO
            return new OkObjectResult("Delete Success");
        }

    }
}

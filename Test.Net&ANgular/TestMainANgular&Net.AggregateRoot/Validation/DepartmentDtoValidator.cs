using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestMainANgular_Net.DTO;

namespace TestMainANgular_Net.AggregateRoot.Validation
{
    public class DepartmentDtoValidator : AbstractValidator<DepartmentDto>
    {
        public DepartmentDtoValidator() 
        {
            RuleFor(x => x.DepartmentFullName)
                .NotEmpty().WithMessage("Department Full Name is Requried.Please Give Department Full Name");
                

            RuleFor(x => x.DepartmentShortName)
                .NotEmpty().WithMessage("Department Short  Name is required.")
                .Length(1, 50).WithMessage("First Name must be between 1 and 50 characters.");
        }
    }
}

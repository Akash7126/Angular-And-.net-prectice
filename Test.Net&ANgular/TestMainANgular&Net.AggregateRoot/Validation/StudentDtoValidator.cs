using FluentValidation;
using TestMainANgular_Net.DTO;

namespace TestMainANgular_Net.AggregateRoot.Validation
{
    public class StudentDtoValidator : AbstractValidator<StudentDto>
    {
        public StudentDtoValidator()
        {
            RuleFor(x => x.RegNo)
                .NotEmpty().WithMessage("Registration Number is required.Please Give Registration NUmber")
                .Length(4, 50).WithMessage("Registration Number must be between 4 and 50 characters.");

            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name must contain only letters and spaces.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .Length(2, 50).WithMessage("Name must be between 2 and 50 characters.")
                .Matches(@"^[a-zA-Z\s]+$").WithMessage("Name must contain only letters and spaces.");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("A valid Email is required.");

            RuleFor(x => x.DateOfBirth)
                .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.");

            RuleFor(x => x.DepartmentId)
                .GreaterThan(0).WithMessage("Department must be selected.");
        }
    }
}

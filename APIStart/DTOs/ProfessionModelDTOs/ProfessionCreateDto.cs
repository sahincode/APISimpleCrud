using FluentValidation;

namespace APIStart.DTOs.ProfessionModelDTOs
{
    public class ProfessionCreateDto
    {
        public string Name { get; set; }
    }
     public class ProfessionCreateDtoValidator:AbstractValidator<ProfessionCreateDto>
    {
        public ProfessionCreateDtoValidator()
        {

            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(50).WithMessage("Can not be greater than 50 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 5 digits");
        }
    }
}

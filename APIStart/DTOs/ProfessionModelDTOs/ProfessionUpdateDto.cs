using FluentValidation;

namespace APIStart.DTOs.ProfessionModelDTOs
{
    public class ProfessionUpdateDto
    {

        public string Name { get; set; }
    }
    public class ProfessionUpdateDtoValidator : AbstractValidator<ProfessionUpdateDto>
    {
        public ProfessionUpdateDtoValidator()
        {

            RuleFor(e => e.Name).NotNull().WithMessage("Can not be null").
                                  NotEmpty().WithMessage("Can not be empty").
                                  MaximumLength(50).WithMessage("Can not be greater than 50 digits").
                                  MinimumLength(3).WithMessage("Can not be less than 5 digits");
        }
    }
}

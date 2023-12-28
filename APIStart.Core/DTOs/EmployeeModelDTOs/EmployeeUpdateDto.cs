﻿

using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace APIStart.Core.DTOs.EmployeeModelDTOs
{
    public class EmployeeUpdateDto
    {
        public string FullName { get; set; }
        public string Description { get; set; }
        public string TwitLink { get; set; }
        public string FaceLink { get; set; }
        public string InstaLink { get; set; }
        public string LinkEdn { get; set; }
        public IFormFile Image { get; set; }
        public double Salary { get; set; }
        public List<int> ProfessionIds { get; set; }

    }
    public class EmployeeUpdateDtoValidator : AbstractValidator<EmployeeUpdateDto>
    {
        public EmployeeUpdateDtoValidator()
        {
            RuleFor(e => e.FullName).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").
                                    MaximumLength(50).WithMessage("Can not be greater than 50 digits").
                                    MinimumLength(5).WithMessage("Can not be less than 5 digits");
            RuleFor(e => e.FaceLink).NotNull().WithMessage("Can not be null").
                                   NotEmpty().WithMessage("Can not be empty").
                                   MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                   MinimumLength(5).WithMessage("Can not be less than 5 digits");
            RuleFor(e => e.TwitLink).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").
                                    MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                    MinimumLength(5).WithMessage("Can not be less than 5 digits");
            RuleFor(e => e.TwitLink).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").
                                    MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                    MinimumLength(5).WithMessage("Can not be less than 5 digits");
            RuleFor(e => e.LinkEdn).NotNull().WithMessage("Can not be null").
                                    NotEmpty().WithMessage("Can not be empty").
                                    MaximumLength(100).WithMessage("Can not be greater than 100 digits").
                                    MinimumLength(5).WithMessage("Can not be less than 5 digits");
            RuleFor(e => e.ProfessionIds).NotNull().WithMessage("Can not be null").
                                  NotEmpty();
        }
    }
}

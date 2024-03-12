using E_HospitalServer.Entities.DTOs;
using FluentValidation;

namespace E_HospitalServer.Business.Validators;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(p=> p.FirstName).NotEmpty().WithMessage("Ad alanı boş olamaz");
        RuleFor(p=> p.FirstName).NotNull().WithMessage("Ad alanı boş olamaz");
        RuleFor(p=> p.FirstName).MinimumLength(3).WithMessage("Ad alanı 3 karakterden uzun olmalı");
    }
}
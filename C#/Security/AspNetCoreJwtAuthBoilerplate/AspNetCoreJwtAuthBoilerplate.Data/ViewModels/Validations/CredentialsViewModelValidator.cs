﻿using FluentValidation;

namespace AspNetCoreJwtAuthBoilerplate.Data.ViewModels.Validations
{
    public class CredentialsViewModelValidator : AbstractValidator<CredentialsViewModel>
    {
        public CredentialsViewModelValidator()
        {
            RuleFor(vm => vm.UserName).NotEmpty().WithMessage("UserName cannot be empty.");
            RuleFor(vm => vm.Password).NotEmpty().WithMessage("Password cannot be empty.");
            RuleFor(vm => vm.Password).Length(6, 12).WithMessage("Password must be between 6 and 12 characters.");
        }
    }
}
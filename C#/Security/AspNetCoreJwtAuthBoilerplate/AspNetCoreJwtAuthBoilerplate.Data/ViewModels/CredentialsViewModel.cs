using AspNetCoreJwtAuthBoilerplate.Data.ViewModels.Validations;
using FluentValidation.Attributes;

namespace AspNetCoreJwtAuthBoilerplate.Data.ViewModels
{
    [Validator(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
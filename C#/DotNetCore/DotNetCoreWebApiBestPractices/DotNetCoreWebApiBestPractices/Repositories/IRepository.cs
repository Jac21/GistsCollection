using DotNetCoreWebApiBestPractices.Controllers;

namespace DotNetCoreWebApiBestPractices.Repositories
{
    public interface IRepository
    {
        Owner Owner { get; set; }
    }
}
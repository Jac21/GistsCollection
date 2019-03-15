using DotNetCoreWebApiBestPractices.Models;

namespace DotNetCoreWebApiBestPractices.Repositories
{
    public interface IRepository
    {
        Owner Owner { get; set; }
    }
}
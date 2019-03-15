using System;

namespace DotNetCoreWebApiBestPractices.Models
{
    public class Owner
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public bool IsActive { get; set; }

        public bool IsObjectNull()
        {
            throw new NotImplementedException();
        }

        public void CreateOwner(Owner owner)
        {
            throw new NotImplementedException();
        }
    }
}
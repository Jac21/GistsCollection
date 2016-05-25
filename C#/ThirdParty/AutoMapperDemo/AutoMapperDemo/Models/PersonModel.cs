using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapperDemo.Models
{
    public class PersonModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public AddressModel Address { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapperDemo.Models
{
    public class PersonDtoModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
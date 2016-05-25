using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapperDemo.Models
{
    public class AddressModel
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
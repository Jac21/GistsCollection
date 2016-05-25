using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapperDemo.Models
{
    public class BookModel
    {
        public string Title { get; set; }
        public AuthorModel Author { get; set; }
        public AuthorFullNameModel AuthorFullName { get; set; }
    }
}
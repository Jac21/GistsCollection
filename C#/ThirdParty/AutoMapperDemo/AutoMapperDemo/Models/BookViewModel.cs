using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AutoMapperDemo.Models
{
    public class BookViewModel
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorFullName { get; set; }
    }
}
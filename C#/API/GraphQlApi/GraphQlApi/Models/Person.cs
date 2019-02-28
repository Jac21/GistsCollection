using System.Collections.Generic;
using System.Text;

namespace GraphQlApi.Models
{
    /// <summary>
    /// Information related to a person: customer, sales person, etc
    /// </summary>
    public class Person
    {
        /// <summary>
        /// The customer Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The customer first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// The customer last name
        /// </summary>
        public string LastName { get; set; }
    }
}

using System;

namespace GraphQlApi.Models
{
    /// <inheritdoc />
    /// <summary>
    /// The employee information
    /// </summary>
    public class Employee : Person
    {
        /// <summary>
        /// The employee title or position in the company
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The date when the employee was hired
        /// </summary>
        public DateTime HiredDate { get; set; }

        /// <summary>
        /// Represents the current department assigned to the employee
        /// </summary>
        public int DepartmentId { get; set; }
    }
}

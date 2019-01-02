using System;

namespace GraphQlApi.Models
{
    /// <summary>
    /// A sales order
    /// </summary>
    public class Order
    {
        /// <summary>
        /// The order Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The order confirmation number
        /// </summary>
        public string ConfirmationNumber { get; set; }

        /// <summary>
        /// The UTC date when the order was created
        /// </summary>
        public DateTime CreatedDateUTC { get; set; }

        /// <summary>
        /// The customer Id :  the information about the person making purchase
        /// </summary>
        public int CustomerId { get; set; }

        /// <summary>
        /// The employee Id: the information about the person helping on this sales order
        /// </summary>
        public int EmployeeId { get; set; }
    }
}

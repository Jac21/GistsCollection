namespace GraphQlApi.Models
{
    /// <summary>
    /// The detailed information about a given order
    /// </summary>
    public class OrderDetail
    {
        /// <summary>
        /// The order detail id: represents one item in the order
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The order related to this order detail
        /// </summary>
        public int OrderId { get; set; }

        /// <summary>
        /// The product Id: The information about the product
        /// </summary>
        public int ProductId { get; set; }

        /// <summary>
        /// The price assigned to the product on current date
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// The amount of products
        /// </summary>
        public int Quantity { get; set; }
    }
}

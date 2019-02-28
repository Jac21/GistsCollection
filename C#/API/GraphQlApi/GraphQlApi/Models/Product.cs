namespace GraphQlApi.Models
{
    /// <summary>
    /// Class that identifies a product item
    /// </summary>
    public class Product
    {
        /// <summary>
        /// The product identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The product full name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The Code can be used as the product abbreviated name, short name or product code as known by third party provider
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The product category Id represents the product categorization
        /// </summary>
        public int ProductCategoryId { get; set; }

        /// <summary>
        /// The provider Id represents the provider entity related to the product
        /// </summary>
        public int ProviderId { get; set; }
    }
}

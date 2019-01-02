namespace GraphQlApi.Models
{
    /// <summary>
    /// The product category
    /// </summary>
    public class ProductCategory
    {
        /// <summary>
        /// The product category Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The product category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// An optional field to identify the parent category
        /// </summary>
        public int? ParentCategoryId { get; set; }
    }
}

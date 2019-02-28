namespace GraphQlApi.Models
{
    /// <inheritdoc />
    /// <summary>
    /// A customer entity
    /// </summary>
    public class Customer : Person
    {
        /// <summary>
        /// The total number of accumulated loyalty points
        /// </summary>
        public int LoyaltyPoints { get; set; }
    }
}

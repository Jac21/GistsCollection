using Couchbase.Linq.Filters;

namespace RestfulJobPattern.Data.Documents
{
    [DocumentTypeFilter(TypeString)]
    public class Star
    {
        private const string TypeString = "star";
        public long Id { get; set; }
        public string Name { get; set; }
        public string Type => TypeString;

        public static string GetKey(long id) => $"{TypeString}-{id}";
    }
}
using Newtonsoft.Json.Linq;

namespace GraphQlApi.CoreApi
{
    /// <summary>
    /// The request containing parameters
    /// </summary>
    public class GraphQlRequest
    {
        public string OperationName { get; set; }
        public string Query { get; set; }
        public JObject Variables { get; set; }
    }
}
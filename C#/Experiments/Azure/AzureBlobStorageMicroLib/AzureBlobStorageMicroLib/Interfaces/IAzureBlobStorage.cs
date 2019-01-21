using System.Threading.Tasks;

namespace AzureBlobStorageMicroLib.Interfaces
{
    public interface IAzureBlobStorage
    {
        string ConnectionString { get; set; }

        Task<IAzureResponse> ProcessAsync(string containerName, bool createNewContainer);
    }
}
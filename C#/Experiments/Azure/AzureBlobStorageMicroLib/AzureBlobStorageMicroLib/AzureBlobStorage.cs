using System;
using System.IO;
using System.Threading.Tasks;
using AzureBlobStorageMicroLib.Interfaces;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace AzureBlobStorageMicroLib
{
    public class AzureBlobStorage : IAzureBlobStorage, ILocalStorage
    {
        public string ConnectionString { get; set; }

        public string LocalFolderPath { get; set; }

        public async Task<IAzureResponse> ProcessAsync(string containerName = "", bool createNewContainer = false)
        {
            if (CloudStorageAccount.TryParse(ConnectionString, out var storageAccount))
            {
                try
                {
                    var blobClient = storageAccount.CreateCloudBlobClient();

                    CloudBlobContainer cloudBlobContainer;

                    if (createNewContainer)
                    {
                        cloudBlobContainer = blobClient.GetContainerReference(containerName ?? "my-new-container");

                        var isCreateSuccess = await cloudBlobContainer.CreateIfNotExistsAsync(
                            BlobContainerPublicAccessType.Container, new BlobRequestOptions(), new OperationContext());

                        if (!isCreateSuccess)
                        {
                            return new AzureResponse
                            {
                                IsSuccess = false,
                                Message = "Failed to create cloud blob container"
                            };
                        }
                    }
                    else
                    {
                        cloudBlobContainer = blobClient.GetContainerReference(containerName);
                    }

                    var permissions = new BlobContainerPermissions
                    {
                        PublicAccess = BlobContainerPublicAccessType.Blob
                    };

                    try
                    {
                        await cloudBlobContainer.SetPermissionsAsync(permissions);
                    }
                    catch (Exception ex)
                    {
                        return new AzureResponse
                        {
                            IsSuccess = false,
                            Message =
                                $"Failed to set cloud blob container permissions. Message: {ex.Message}"
                        };
                    }

                    foreach (var filePath in Directory.GetFiles(LocalFolderPath, "*.*", SearchOption.AllDirectories))
                    {
                        var blob = cloudBlobContainer.GetBlockBlobReference(filePath);

                        try
                        {
                            await blob.UploadFromFileAsync(filePath);
                        }
                        catch (Exception ex)
                        {
                            return new AzureResponse
                            {
                                IsSuccess = false,
                                Exception = ex,
                                Message = $"Failed to upload {filePath}, Exception: {ex.Message}",
                            };
                        }
                    }
                }
                catch (StorageException ex)
                {
                    return new AzureResponse
                    {
                        IsSuccess = false,
                        Exception = ex,
                        Message = $"Error returned from the service: {ex.Message}"
                    };
                }
            }
            else
            {
                return new AzureResponse
                {
                    IsSuccess = false,
                    Message = "Failed to parse connection string!"
                };
            }

            return new AzureResponse
            {
                IsSuccess = true
            };
        }
    }
}
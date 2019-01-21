using System;

namespace AzureBlobStorageMicroLib.Interfaces
{
    public interface IAzureResponse
    {
        Exception Exception { get; set; }

        bool IsSuccess { get; set; }

        string Message { get; set; }

        object Result { get; set; }
    }
}
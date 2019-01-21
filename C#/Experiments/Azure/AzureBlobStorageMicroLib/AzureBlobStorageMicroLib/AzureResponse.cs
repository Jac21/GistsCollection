using System;
using AzureBlobStorageMicroLib.Interfaces;

namespace AzureBlobStorageMicroLib
{
    public class AzureResponse : IAzureResponse
    {
        public Exception Exception { get; set; }
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public object Result { get; set; }
    }
}
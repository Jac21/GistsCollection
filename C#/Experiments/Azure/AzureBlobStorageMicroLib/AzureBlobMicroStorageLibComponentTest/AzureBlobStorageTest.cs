using AzureBlobStorageMicroLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AzureBlobMicroStorageLibComponentTest
{
    [TestClass]
    public class AzureBlobStorageTest
    {
        readonly AzureBlobStorage azureBlobStorage = new AzureBlobStorage();

        [TestMethod]
        public void ProcessAsyncWithExistingContainerIsSuccess()
        {
            // arrange
            azureBlobStorage.LocalFolderPath = @"D:\\Azure\\LocalFiles";
            azureBlobStorage.ConnectionString = "<copy your storage account connection string here>";

            // act
            var serviceResponse = azureBlobStorage.ProcessAsync("my-container");

            // assert
            Assert.IsTrue(serviceResponse.Result.IsSuccess,
                $"Failed to upload files! Service response: {serviceResponse.Result.Message}");
        }

        [TestMethod]
        public void ProcessAsyncWithNewContainerIsSuccess()
        {
            // arrange
            azureBlobStorage.LocalFolderPath = @"D:\\Azure\\LocalFiles";
            azureBlobStorage.ConnectionString = "<copy your storage account connection string here>";

            // act
            var serviceResponse = azureBlobStorage.ProcessAsync("my-container-new", true);

            // assert
            Assert.IsTrue(serviceResponse.Result.IsSuccess,
                $"Failed to upload files! Service response: {serviceResponse.Result.Message}");
        }
    }
}
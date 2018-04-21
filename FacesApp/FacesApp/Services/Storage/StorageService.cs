using FacesApp.Helpers;
using FacesApp.Models;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;

namespace FacesApp.Services.Storage
{
    public class StorageService : IStorageService
    {
        public async Task UploadPhotoAsync(Photo photoInfo)
        {
            CloudBlobContainer container = GetContainer();
            CloudBlockBlob fileBlob = container.GetBlockBlobReference(photoInfo.Name);
            await fileBlob.UploadFromByteArrayAsync(photoInfo.PhotoData, 0, photoInfo.PhotoData.Length);
        }

        private CloudBlobContainer GetContainer()
        {
            CloudStorageAccount account = CloudStorageAccount.Parse(Constants.StorageEndPoint);
            CloudBlobClient client = account.CreateCloudBlobClient();
            return client.GetContainerReference(Constants.ContainerName);
        }
    }
}
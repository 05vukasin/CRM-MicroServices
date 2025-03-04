using Azure.Storage.Blobs;

namespace CRM.ProductService.Services
{
    public class ImageService
    {
        private readonly BlobServiceClient _blobServiceClient;

        public ImageService(BlobServiceClient blobServiceClient)
        {
            _blobServiceClient = blobServiceClient;
        }

        public async Task<string> UploadImageAsync(string fileName, Stream fileStream)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("slike");

            // Generišite jedinstveno ime za fajl.
            string uniqueFileName = GenerateUniqueFileName(fileName);

            var blobClient = blobContainer.GetBlobClient(uniqueFileName);
            await blobClient.UploadAsync(fileStream, overwrite: true);

            // Vratite potpuni URL fajla
            return blobClient.Uri.ToString();
        }

        public async Task DeleteImageAsync(string fileName)
        {
            var blobContainer = _blobServiceClient.GetBlobContainerClient("slike");
            var blobClient = blobContainer.GetBlobClient(fileName);
            await blobClient.DeleteIfExistsAsync();
        }

        private string GenerateUniqueFileName(string originalFileName)
        {
            // Dodajte GUID pre ekstenzije fajla
            string extension = Path.GetExtension(originalFileName); // Ekstenzija fajla (.png, .jpg itd.)
            string baseName = Path.GetFileNameWithoutExtension(originalFileName); // Ime fajla bez ekstenzije
            string uniqueId = Guid.NewGuid().ToString(); // Jedinstveni GUID

            return $"{baseName}_{uniqueId}{extension}";
        }
    }
}

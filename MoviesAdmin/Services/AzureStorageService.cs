using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using Azure.Storage.Sas; 

namespace MoviesAdmin.Services
{
    public class AzureStorageService : IAzureStorageService
    {
        private readonly IConfiguration _configuration;
        private readonly BlobServiceClient _blobServiceClient;
        private string containerName;

        public AzureStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
            containerName = _configuration["AzureStorage:ContainerName"] ?? throw new InvalidOperationException("Missing configuration");
            string connectionString = _configuration["AzureStorage:ConnectionString"] ?? throw new InvalidOperationException("Missing configuration");
            _blobServiceClient = new BlobServiceClient(connectionString);
        }

        public async Task<string> UploadFileAsync(IFormFile file, string filename)
        {
            var containerClient = _blobServiceClient.GetBlobContainerClient(containerName);
            await containerClient.CreateIfNotExistsAsync();

            var blobClient = containerClient.GetBlobClient(filename);

            var options = new BlobUploadOptions
            {
                HttpHeaders = new BlobHttpHeaders
                {
                    ContentType = file.ContentType
                }
            };

            using (var stream = file.OpenReadStream())
            {
                await blobClient.UploadAsync(stream, options);
            }

            if (blobClient.CanGenerateSasUri)
            {
                var sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = containerName,
                    BlobName = filename,
                    Resource = "b",
                    ExpiresOn = DateTimeOffset.UtcNow.AddYears(1) 
                };
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                return sasUri.ToString();
            }

            return blobClient.Uri.ToString();
        }
    }
}
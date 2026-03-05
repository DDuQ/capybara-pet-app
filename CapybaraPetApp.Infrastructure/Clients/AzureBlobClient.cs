using Azure.Storage.Blobs;
using CapybaraPetApp.Application.Abstractions.Clients;

namespace CapybaraPetApp.Infrastructure.Clients;

public class AzureBlobClient(BlobServiceClient capybuddyBlobServiceClient) : IAzureBlobClient
{
    private const int DefaultImageId = 1;
    
    public string GetUserProfilePicture(Guid? userId, bool isDefault)
    { 
        var containerClient = capybuddyBlobServiceClient.GetBlobContainerClient("capybuddy-profile-pics");

        if(isDefault)
        {
            var defaultBlobs = containerClient
                .FindBlobsByTags($"imageId = '{DefaultImageId}'")
                .AsPages();

            var defaultBlobClient = containerClient.GetBlobClient(defaultBlobs.First().Values[0].BlobName);
            return defaultBlobClient.Uri.ToString();
        }

        var blobs = containerClient
            .FindBlobsByTags($"imageId = '{userId}'")
            .AsPages();

        var blobClient = containerClient.GetBlobClient(blobs.First().Values[0].BlobName);

        return blobClient.Uri.ToString();
    }
}

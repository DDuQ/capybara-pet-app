namespace CapybaraPetApp.Application.Abstractions.Clients;

public interface IAzureBlobClient
{
    string GetUserProfilePicture(Guid? imageId, bool isDefault);
}

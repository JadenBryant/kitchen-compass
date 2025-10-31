namespace CustomerBackend.Services;

using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

public class GoogleCloudStorageService {
    private readonly StorageClient _storageClient;
    private readonly string _bucketName = "kitchen-compass-images";
    private readonly GoogleCredential _credential;

    public GoogleCloudStorageService() {
        var base64 = Environment.GetEnvironmentVariable("GOOGLE_CREDENTIALS_B64");

        if (string.IsNullOrEmpty(base64))
            throw new InvalidOperationException("Missing GOOGLE_CREDENTIALS_B64 environment variable");

        var jsonBytes = Convert.FromBase64String(base64);
        using var stream = new MemoryStream(jsonBytes);

        _credential = GoogleCredential.FromStream(stream);
        _storageClient = StorageClient.Create(_credential);
    }

    // Upload file (as before)
    public async Task<string> UploadImageAsync(IFormFile file) {
        using var memoryStream = new MemoryStream();
        await file.CopyToAsync(memoryStream);
        memoryStream.Position = 0;

        var objectName = $"{Guid.NewGuid()}_{file.FileName}";
        await _storageClient.UploadObjectAsync(_bucketName, objectName, file.ContentType, memoryStream);

        return objectName; // Return the object name instead of full URL
    }

    // Generate a signed URL for private download
    public string GetSignedUrl(string objectName, int minutesValid = 15) {
        // Create a URL signer from the same service account
        var signer = UrlSigner.FromCredential(_credential);
        var expiration = TimeSpan.FromMinutes(minutesValid);

        return signer.Sign(_bucketName, objectName, expiration, HttpMethod.Get);
    }

    // Delete image
    public async Task DeleteImageAsync(string objectName) {
        await _storageClient.DeleteObjectAsync(_bucketName, objectName);
    }
}
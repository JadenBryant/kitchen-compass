using CustomerBackend.Services;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Initialize Firebase Admin SDK
var base64 = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIALS_B64");

if (string.IsNullOrEmpty(base64)) {
    throw new InvalidOperationException("Missing FIREBASE_CREDENTIALS_B64 environment variable");
}

var jsonBytes = Convert.FromBase64String(base64);
using var stream = new MemoryStream(jsonBytes);

FirebaseApp.Create(new AppOptions() {
    Credential = GoogleCredential.FromStream(stream),
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register GoogleCloudStorageService as a singleton for image handling
builder.Services.AddSingleton<GoogleCloudStorageService>();

builder.Services.AddSingleton<IShoppingCartService, ShoppingCartService>();
builder.Services.AddSingleton<MenuService>();

var app = builder.Build();

// Display Swagger UI in production
app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

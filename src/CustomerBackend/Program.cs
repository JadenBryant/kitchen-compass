using CustomerBackend.Services;
using CustomerBackend.Models;
using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

// Create the web application builder.
var builder = WebApplication.CreateBuilder(args);

// Register GUID serializer for MongoDB.
MongoDB.Bson.Serialization.BsonSerializer.RegisterSerializer(
    new MongoDB.Bson.Serialization.Serializers.GuidSerializer(MongoDB.Bson.GuidRepresentation.Standard)
);

// Initialize Firebase Admin SDK with credentials from environment variable.
var base64 = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIALS_B64");
if (string.IsNullOrEmpty(base64)) {
    throw new InvalidOperationException("Missing FIREBASE_CREDENTIALS_B64 environment variable");
}

var jsonBytes = Convert.FromBase64String(base64);
using var stream = new MemoryStream(jsonBytes);
FirebaseApp.Create(new AppOptions() {
    Credential = GoogleCredential.FromStream(stream),
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure database settings from configuration.
builder.Services.Configure<DatabaseSettings>(
    builder.Configuration.GetSection("DatabaseSettings"));

// Register services as singletons.
builder.Services.AddSingleton<GoogleCloudStorageService>();
builder.Services.AddSingleton<IShoppingCartService, ShoppingCartService>();
builder.Services.AddSingleton<MenuService>();
builder.Services.AddSingleton<MenuItemService>();

// Configure MongoDB client and database.
builder.Services.AddSingleton<IMongoClient>(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    return new MongoClient(settings.ConnectionString);
});

// Register MongoDB database instance.
builder.Services.AddSingleton(sp =>
{
    var settings = sp.GetRequiredService<IOptions<DatabaseSettings>>().Value;
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase(settings.DatabaseName);
});

// Build the application.
var app = builder.Build();

// Display Swagger UI.
app.UseSwagger();
app.UseSwaggerUI();

// Configure the HTTP request pipeline.
// app.UseHttpsRedirection();
app.UseAuthorization();

// Map controller routes.
app.MapControllers();

// Run the application.
app.Run();

using FirebaseAdmin;
using Google.Apis.Auth.OAuth2;

var builder = WebApplication.CreateBuilder(args);

// Initialize Firebase Admin SDK
var json = Environment.GetEnvironmentVariable("FIREBASE_CREDENTIAL_JSON");
FirebaseApp.Create(new AppOptions() {
    Credential = GoogleCredential.FromJson(json)
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

app.Run();

using CustomerBackend.DTOs.User;
using CustomerBackend.Models;
using CustomerBackend.Services;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CustomerBackend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly string _firebaseApiKey;
    private readonly JsonSerializerOptions _jsonOptions;
    private readonly UserService _userService;
    
    public AuthController(IConfiguration config, UserService userService)
    {
        _httpClient = new HttpClient();
        _firebaseApiKey = config["FIREBASE_API_KEY"] ?? throw new Exception("Missing FIREBASE_API_KEY");
        _userService = userService;

        _jsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request)
    {
        var payload = new
        {
            email = request.Email,
            password = request.Password,
            returnSecureToken = true
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"https://identitytoolkit.googleapis.com/v1/accounts:signUp?key={_firebaseApiKey}",
            payload,
            _jsonOptions
        );

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(content, _jsonOptions);

        if (response.IsSuccessStatusCode) {
            if (json.TryGetProperty("localId", out var localIdProp) && localIdProp.ValueKind == JsonValueKind.String) {
                var firebaseId = localIdProp.GetString() ?? "";

                var user = new User {
                    FirebaseId = firebaseId,
                    Email = request.Email,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    JoinDate = DateTime.UtcNow
                };

                await _userService.CreateAsync(user);
            }
        }


        return StatusCode((int)response.StatusCode, json);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        var payload = new
        {
            email = request.Email,
            password = request.Password,
            returnSecureToken = true
        };

        var response = await _httpClient.PostAsJsonAsync(
            $"https://identitytoolkit.googleapis.com/v1/accounts:signInWithPassword?key={_firebaseApiKey}",
            payload,
            _jsonOptions
        );

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(content, _jsonOptions);
        return StatusCode((int)response.StatusCode, json);
    }

    [HttpPost("verify")]
    public async Task<IActionResult> VerifyToken([FromBody] VerifyRequest request)
    {
        var payload = new { idToken = request.IdToken };

        var response = await _httpClient.PostAsJsonAsync(
            $"https://identitytoolkit.googleapis.com/v1/accounts:lookup?key={_firebaseApiKey}",
            payload,
            _jsonOptions
        );

        var content = await response.Content.ReadAsStringAsync();
        var json = JsonSerializer.Deserialize<JsonElement>(content, _jsonOptions);
        return StatusCode((int)response.StatusCode, json);
    }
}

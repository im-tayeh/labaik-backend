using System.Security.Cryptography;

// Generates a cryptographically random signing key for JWT (HMAC-SHA256 needs at least 32 bytes; we use 64).
// Usage: dotnet run tools/generate-key.cs
var key = RandomNumberGenerator.GetBytes(64);

Console.WriteLine(Convert.ToBase64String(key));

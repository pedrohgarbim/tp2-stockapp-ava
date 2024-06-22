using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using StockApp.API;
using StockApp.Application.DTOs;
using Xunit;

namespace StockApp.Domain.Test
{
    public class UserAuthenticationTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public UserAuthenticationTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task RegisterAndLogin_ValidCredentials_ReturnsToken()
        {
            var login = new UserLoginDto
            {
                Username = "name",
                Password = "password"
            };

            var register = new UserRegisterDto
            {
                Username = "user",
                Password = "password",
                Role = "User",
            };

            var loginResponse = await _client.PostAsJsonAsync("/api/token/login", login);
            loginResponse.EnsureSuccessStatusCode();

            var registerResponse = await _client.PostAsJsonAsync("/api/token/register", register);
            registerResponse.EnsureSuccessStatusCode();

            var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponseDTO>();

            Assert.NotNull(tokenResponse);
            Assert.NotNull(tokenResponse.Token);
            Assert.True(tokenResponse.Expiration > DateTime.UtcNow);
        }
    }
}


using BCrypt.Net;
using Microsoft.Extensions.Configuration;
using Moq;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Application.Services;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Test
{
    public class AuthServiceTests
    {
        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var configurationMock = new Mock<IAppConfiguration>();
            var authService = new AuthService(userRepositoryMock.Object, configurationMock.Object);

            var testUser = new User
            {
                Username = "testUser",
                PasswordHash = BCrypt.Net.BCrypt.HashPassword("password"),
                Role = "user"
            };

            userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(testUser);
            configurationMock.Setup(config => config["Jwt:Key"]).Returns("3xmpl3V3ryS3cur3S3cr3tK3y!@#123");
            configurationMock.Setup(config => config["Jwt:ExpirationMinutes"]).Returns("60");

            var result = await authService.AuthenticateAsync("testUser", "password");

            Assert.NotNull(result);
            Assert.IsType<TokenResponseDTO>(result);
            Assert.False(string.IsNullOrEmpty(result.Token));
            Assert.True(result.Expiration > DateTime.UtcNow);
        }

        [Fact]
        public async Task AuthenticateAsync_InvalidCredentials_ReturnsNull()
        {
            var userRepositoryMock = new Mock<IUserRepository>();
            var configurationMock = new Mock<IAppConfiguration>();
            var authService = new AuthService(userRepositoryMock.Object, configurationMock.Object);

            userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var result = await authService.AuthenticateAsync("wronguser", "wrongpassword");

            Assert.Null(result);
        }
    }
}

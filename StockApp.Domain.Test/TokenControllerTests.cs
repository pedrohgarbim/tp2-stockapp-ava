using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.Domain.Test
{
    public class TokenControllerTests
    {
        private readonly Mock<IAuthService> _authServiceMock;
        private readonly TokenController _tokenController;

        public TokenControllerTests()
        {
            _authServiceMock = new Mock<IAuthService>();
            _tokenController = new TokenController(_authServiceMock.Object);
        }

        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            _authServiceMock.Setup(services => services.AuthenticateAsync(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(new TokenResponseDTO
            {
                Token = "token",
                Expiration = DateTime.UtcNow.AddMinutes(60),
            });
            var userLoginDto = new UserLoginDto
            {
                Username = "testuser",
                Password = "password",
            };

            var result = await _tokenController.Login(userLoginDto) as OkObjectResult;

            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<TokenResponseDTO>(result.Value);
        }

        [Fact]
        public async Task Login_EmptyCredentials_ReturnBadRequest()
        {
            var userLoginDto = new UserLoginDto
            {
                Username = "",
                Password = "",
            };

            var result = await _tokenController.Login(userLoginDto) as BadRequestObjectResult;

            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}

using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using StockApp.API.Controllers;
using StockApp.API.Resources;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Threading.Tasks;
using Xunit;

namespace StockApp.Domain.Test.Controllers
{
    public class UserControllerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserAuditService> _userAuditServiceMock;
        private readonly Mock<IStringLocalizer<SharedResource>> _localizerMock;
        private readonly Mock<IEmailService> _emailServiceMock;
        private readonly UserController _userController;

        public UserControllerTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userAuditServiceMock = new Mock<IUserAuditService>();
            _localizerMock = new Mock<IStringLocalizer<SharedResource>>();
            _emailServiceMock = new Mock<IEmailService>();

            _userController = new UserController(
                _userRepositoryMock.Object,
                _userAuditServiceMock.Object,
                _localizerMock.Object,
                _emailServiceMock.Object
            );
        }

        [Fact]
        public async Task Register_ValidUser_ReturnsOk()
        {
            // Arrange
            var userRegisterDto = new UserRegisterDto
            {
                Username = "testuser",
                Password = "password",
                Role = "User"
            };

            // Act
            var result = await _userController.Register(userRegisterDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task ForgotPassword_ValidUser_ReturnsOk()
        {
            // Arrange
            var user = new User { Username = "testuser", Email = "test@example.com" };
            _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);

            var forgotPasswordDto = new ForgotPasswordDto
            {
                Username = "testuser",
                Email = "test@example.com"
            };

            // Act
            var result = await _userController.ForgotPassword(forgotPasswordDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            _emailServiceMock.Verify(email => email.SendEmailAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()), Times.Once);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task ForgotPassword_InvalidUser_ReturnsNotFound()
        {
            // Arrange
            _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync((User)null);

            var forgotPasswordDto = new ForgotPasswordDto
            {
                Username = "nonexistentuser",
                Email = "wrong@example.com"
            };

            _localizerMock.Setup(_ => _["UserNotFound"]).Returns(new LocalizedString("UserNotFound", "User not found"));

            // Act
            var result = await _userController.ForgotPassword(forgotPasswordDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("User not found", notFoundResult.Value);
        }

        [Fact]
        public async Task ResetPassword_ValidToken_ReturnsOk()
        {
            // Arrange
            var user = new User { Username = "testuser", PasswordResetToken = "validtoken" };
            _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);

            var resetPasswordDto = new ResetPasswordDto
            {
                Username = "testuser",
                Token = "validtoken",
                NewPassword = "newpassword"
            };

            // Act
            var result = await _userController.ResetPassword(resetPasswordDto);

            // Assert
            var okResult = Assert.IsType<OkResult>(result);
            Assert.Equal(200, okResult.StatusCode);
            _userRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<User>()), Times.Once);
        }

        [Fact]
        public async Task ResetPassword_InvalidToken_ReturnsNotFound()
        {
            // Arrange
            var user = new User { Username = "testuser", PasswordResetToken = "othertoken" };
            _userRepositoryMock.Setup(repo => repo.GetByUsernameAsync(It.IsAny<string>())).ReturnsAsync(user);

            var resetPasswordDto = new ResetPasswordDto
            {
                Username = "testuser",
                Token = "invalidtoken",
                NewPassword = "newpassword"
            };

            _localizerMock.Setup(_ => _["InvalidToken"]).Returns(new LocalizedString("InvalidToken", "Invalid token"));

            // Act
            var result = await _userController.ResetPassword(resetPasswordDto);

            // Assert
            var notFoundResult = Assert.IsType<NotFoundObjectResult>(result);
            Assert.Equal(404, notFoundResult.StatusCode);
            Assert.Equal("Invalid token", notFoundResult.Value);
        }
    }
}

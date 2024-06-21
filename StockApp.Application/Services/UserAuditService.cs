using Microsoft.Extensions.Logging;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;
using System;

namespace StockApp.Application.Services
{
    public class UserAuditService : IUserAuditService
    {
        private readonly ILogger<UserAuditService> _logger;

        public UserAuditService(ILogger<UserAuditService> logger)
        {
            _logger = logger;
        }

        public void LogUserAction(string username, string action)
        {
            _logger.LogInformation($"User: {username} performed action: {action} at {DateTime.UtcNow}");

        }
    }
}

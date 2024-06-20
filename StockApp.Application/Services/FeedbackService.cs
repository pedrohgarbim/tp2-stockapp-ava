using StockApp.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StockApp.Domain.Entities;
using AutoMapper;
using Microsoft.Extensions.Logging;
using StockApp.Infra.Data.Context;


namespace StockApp.Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly ISentimentAnalysisService _sentimentAnalysisService;
        private readonly ApplicationDbContext _dbContext;
        private readonly ILogger<FeedbackService> _logger;

        public FeedbackService(ISentimentAnalysisService sentimentAnalysisService, ApplicationDbContext dbContext, ILogger<FeedbackService> logger)
        {
            _sentimentAnalysisService = sentimentAnalysisService;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task SubmitFeedbackAsync(string userId, string feedback)
        {
            try
            {
                var sentiment = await _sentimentAnalysisService.AnalyzeSentimentAsync(feedback);

                var feedbackEntry = new Feedback
                {
                    UserId = userId,
                    Content = feedback,
                    Sentiment = sentiment,
                    Timestamp = DateTime.UtcNow
                };
                await _dbContext.Feedbacks.AddAsync(feedbackEntry);
                await _dbContext.SaveChangesAsync();

                _logger.LogInformation("Feedback submitted successfully for user {UserId}", userId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error submitting feedback for user {UserId}", userId);
                throw;
            }
        }
    }
}

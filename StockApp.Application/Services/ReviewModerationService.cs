using StockApp.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StockApp.Application.Services
{
    public class ReviewModerationService : IReviewModerationService
    {
        private readonly List<string> _inappropriateWords = new List<string> { "inapropriado", "ofensivo", "abusivo" };

        public bool ModerateReview(string review)
        {
            foreach (var word in _inappropriateWords)
            {
                if (Regex.IsMatch(review, @"\b" + word + @"\b", RegexOptions.IgnoreCase))
                {
                    return false;
                }           
            }
            return true;    
        }
    }
}

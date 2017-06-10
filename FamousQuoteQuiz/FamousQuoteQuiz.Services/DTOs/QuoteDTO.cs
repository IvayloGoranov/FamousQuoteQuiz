using System;
using System.Linq.Expressions;

using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.Services.DTOs
{
    public class QuoteDTO
    {
        public static Expression<Func<Quote, QuoteDTO>> MapToDTO
        {
            get
            {
                return x => new QuoteDTO
                {
                    Content = x.Content,
                    Author = x.Author.Name
                };
            }
        }

        public string Content { get; set; }

        public string Author { get; set; }
    }
}

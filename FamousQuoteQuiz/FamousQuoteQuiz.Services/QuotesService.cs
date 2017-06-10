using System;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

using FamousQuoteQuiz.Data.Repositories;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Services.DTOs;

namespace FamousQuoteQuiz.Services
{
    public class QuotesService : IQuotesService
    {
        private readonly Random randomizer = new Random();

        private IRepository<Quote> quotesRepository;

        public QuotesService(IRepository<Quote> quotesRepository)
        {
            this.quotesRepository = quotesRepository;
        }

        public async Task<QuoteDTO> GetRandomQuote()
        {
            int quotesCount = this.quotesRepository.GetAll().Count();

            if (quotesCount == 0)
            {
                throw new InvalidOperationException(
                    "No quotes in the database. Please populate db with quotes first.");
            }

            int randomQuoteId = this.randomizer.Next(1, quotesCount + 1);

            var randomQuote = await this.quotesRepository
                                        .GetAll()
                                        .Include(x => x.Author)
                                        .Where(x => x.Id == randomQuoteId)
                                        .Select(QuoteDTO.MapToDTO)
                                        .FirstOrDefaultAsync();

            return randomQuote;
        }
    }
}

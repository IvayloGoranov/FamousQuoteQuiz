using System;
using System.Threading.Tasks;
using System.Linq;
using System.Data.Entity;

using FamousQuoteQuiz.Data.Repositories;
using FamousQuoteQuiz.Models;
using FamousQuoteQuiz.Services.DTOs;
using FamousQuoteQuiz.Utils;

namespace FamousQuoteQuiz.Services
{
    public class QuotesService : IQuotesService
    {
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

            int randomQuoteId = StaticRandomizer.RandomNumber(1, quotesCount + 1);

            var randomQuote = await this.quotesRepository
                                        .GetAll()
                                        .Include(x => x.Author)
                                        .Where(x => x.Id == randomQuoteId)
                                        .Select(QuoteDTO.MapToDTO)
                                        .FirstOrDefaultAsync();

            return randomQuote;
        }

        public async Task<QuoteDTO> GetQuoteById(int id)
        {
            var quote = await this.quotesRepository
                                        .GetAll()
                                        .Include(x => x.Author)
                                        .Where(x => x.Id == id)
                                        .Select(QuoteDTO.MapToDTO)
                                        .FirstOrDefaultAsync();

            return quote;
        }
    }
}

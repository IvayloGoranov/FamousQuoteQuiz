using System.Data.Entity;
using System.Threading.Tasks;
using System.Data.Entity.Infrastructure;

using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.Data
{
    public interface IQuoteQuizContext
    {
       IDbSet<Author> Authors { get; set; }

        IDbSet<Quote> Quotes { get; set; }

        IDbSet<QuizMode> QuizModes { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DbEntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        int SaveChanges();

        Task<int> SaveChangesAsync();
    }
}

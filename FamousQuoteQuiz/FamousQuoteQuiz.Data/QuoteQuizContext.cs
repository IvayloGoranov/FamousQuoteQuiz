using System.Data.Entity;
using System.Threading.Tasks;

using FamousQuoteQuiz.Models;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace FamousQuoteQuiz.Data
{
    public class QuoteQuizContext : DbContext, IQuoteQuizContext
    {
        public QuoteQuizContext()
            : base("name=QuoteQuizContext")
        {
        }

        public IDbSet<Author> Authors { get; set; }

        public IDbSet<Quote> Quotes { get; set; }

        public IDbSet<QuizMode> QuizModes { get; set; }

        public IDbSet<Answer> Answers { get; set; }

        public IDbSet<Question> Questions { get; set; }

        public IDbSet<Quizz> Quizzes { get; set; }

        public static QuoteQuizContext Create()
        {
            return new QuoteQuizContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();

            modelBuilder.Entity<Question>()
                   .HasRequired(x => x.Answers)
                   .WithRequiredPrincipal(x => x.Question);

            modelBuilder.Entity<Answer>()
                .HasMany<Author>(s => s.Authors)
                .WithMany(c => c.Answers)
                .Map(cs =>
                {
                    cs.MapLeftKey("AnswerId");
                    cs.MapRightKey("AuthorId");
                    cs.ToTable("Answers_Authors");
                });

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}
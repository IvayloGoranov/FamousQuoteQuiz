using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousQuoteQuiz.Models
{
    public class Question : BaseModel<int>
    {
        private ICollection<Quizz> quizzes;
        private ICollection<Answer> answers;

        public Question()
        {
            this.quizzes = new HashSet<Quizz>();
            this.answers = new HashSet<Answer>();
        }

        [ForeignKey("Quote")]
        public int QuoteID { get; set; }

        public virtual Quote Quote { get; set; }

        public virtual Answer Answers { get; set; }

        public virtual ICollection<Quizz> Quizzes
        {
            get
            {
                return this.quizzes;
            }

            set
            {
                this.quizzes = value;
            }
        }
    }
}

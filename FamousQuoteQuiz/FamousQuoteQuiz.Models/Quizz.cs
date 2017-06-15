using System.Collections.Generic;

namespace FamousQuoteQuiz.Models
{
    public class Quizz : BaseModel<int>
    {
        private ICollection<Question> questions;

        public Quizz()
        {
            this.questions = new HashSet<Question>();
        }

        public virtual ICollection<Question> Questions
        {
            get
            {
                return this.questions;
            }

            set
            {
                this.questions = value;
            }
        }
    }
}


using System.Collections.Generic;

namespace FamousQuoteQuiz.Models
{
    public class Answer : BaseModel<int>
    {
        private ICollection<Author> authors;

        public Answer()
        {
            this.authors = new HashSet<Author>();
        }

        public bool UserAnswer { get; set; }

        public virtual Question Question { get; set; }

        public virtual ICollection<Author> Authors
        {
            get
            {
                return this.authors;
            }

            set
            {
                this.authors = value;
            }
        }
    }
}

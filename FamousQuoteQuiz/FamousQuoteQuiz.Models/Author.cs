using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Models
{
    public class Author : BaseModel<int>
    {
        private ICollection<Quote> quotes;
        private ICollection<Answer> answers;

        public Author()
        {
            this.quotes = new HashSet<Quote>();
            this.answers = new HashSet<Answer>();
        }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} name must be between {1} and {2} characters long.",
            MinimumLength = 1)]
        public string Name { get; set; }

        public virtual ICollection<Quote> Quotes
        {
            get
            {
                return this.quotes;
            }

            set
            {
                this.quotes = value;
            }
        }

        public virtual ICollection<Answer> Answers
        {
            get
            {
                return this.answers;
            }

            set
            {
                this.answers = value;
            }
        }
    }
}

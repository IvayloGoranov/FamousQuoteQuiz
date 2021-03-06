﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousQuoteQuiz.Models
{
    public class Quote : BaseModel<int>
    {
        private ICollection<Question> questions;

        public Quote()
        {
            this.questions = new HashSet<Question>();
        }

        [Required]
        public string Content { get; set; }

        [ForeignKey("Author")]
        public int AuthorID { get; set; }

        public virtual Author Author { get; set; }

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

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FamousQuoteQuiz.Models
{
    public class Quote : BaseModel<int>
    {
        [Required]
        public string Content { get; set; }

        [ForeignKey("Author")]
        [Required]
        public string AuthorID { get; set; }

        public virtual Author Author { get; set; }
    }
}

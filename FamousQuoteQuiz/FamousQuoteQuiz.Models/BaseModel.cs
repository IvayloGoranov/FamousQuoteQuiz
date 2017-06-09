using System.ComponentModel.DataAnnotations;

namespace FamousQuoteQuiz.Models
{
    public abstract class BaseModel<TKey>
    {
        [Key]
        public TKey Id { get; set; }
    }
}

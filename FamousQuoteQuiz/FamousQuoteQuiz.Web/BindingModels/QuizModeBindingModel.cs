using System.ComponentModel.DataAnnotations;

using FamousQuoteQuiz.Models;

namespace FamousQuoteQuiz.Web.BindingModels
{
    public class QuizModeBindingModel
    {
        [Required, Display(Name = "Quiz mode type")]
        public QuizModeType Type { get; set; }
    }
}
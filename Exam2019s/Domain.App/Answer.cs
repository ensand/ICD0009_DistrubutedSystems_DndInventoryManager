using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.App
{
    public class Answer
    {
        public Guid Id { get; set; } = default!;

        [JsonIgnore]
        public MultipleChoiceQuestion? MultipleChoiceQuestion { get; set; }
        public Guid MultipleChoiceQuestionId { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string AnswerValue { get; set; } = default!;

        public bool IsCorrect { get; set; }
    }
}
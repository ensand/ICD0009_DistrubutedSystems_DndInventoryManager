using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Domain.App
{
    public class MultipleChoiceQuestion
    {
        public Guid Id { get; set; } = default!;

        [JsonIgnore]
        public Quiz? Quiz { get; set; }
        public Guid QuizId { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string Question { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string ExpectedAnswer { get; set; } = default!;

        public ICollection<Answer>? Answers { get; set; }
    }
}
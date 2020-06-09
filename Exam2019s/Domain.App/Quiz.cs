using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;

namespace Domain.App
{
    public class Quiz
    {
        public Guid Id { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(512)]
        [Required]
        public string Title { get; set; } = default!;

        public ICollection<MultipleChoiceQuestion>? MultipleChoiceQuestions { get; set; }
        public ICollection<TextEntryQuestion>? TextEntryQuestions { get; set; }
    }
}
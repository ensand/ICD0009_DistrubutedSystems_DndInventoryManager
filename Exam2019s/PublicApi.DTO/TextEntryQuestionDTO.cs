using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class TextEntryQuestionDTO
    {
        public Guid Id { get; set; } = default!;

        public string Question { get; set; } = default!;
    }

    public class CreateTextEntryQuestionDTO
    {
        public Guid QuizId { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string Question { get; set; } = default!;
    }

    public class TextEntryQuestionWithAnswersDTO
    {
        public Guid Id { get; set; } = default!;

        public string Question { get; set; } = default!;

        public ICollection<TextEntryAnswerDTO> Answers { get; set; } = new List<TextEntryAnswerDTO>();
    }
}
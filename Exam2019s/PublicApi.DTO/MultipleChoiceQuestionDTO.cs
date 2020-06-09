using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class MultipleChoiceQuestionDTO
    {
        public Guid Id { get; set; } = default!;

        public string Question { get; set; } = default!;

        public string ExpectedAnswer { get; set; } = default!;

        // public ICollection<AnswerDTO>? Answers { get; set; }
    }

    public class EditMultipleChoiceQuestionDTO
    {
        public Guid Id { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string Question { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string ExpectedAnswer { get; set; } = default!;
    }

    public class CreateMultipleChoiceQuestionDTO
    {
        public Guid QuizId { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string Question { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(1024)]
        [Required]
        public string ExpectedAnswer { get; set; } = default!;
    }
}
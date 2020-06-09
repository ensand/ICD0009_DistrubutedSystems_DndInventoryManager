using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class QuizDTO
    {
        public Guid Id { get; set; } = default!;
        
        public string Title { get; set; } = default!;
        
        // public ICollection<MultipleChoiceQuestionDTO> MultipleChoiceQuestions { get; set; }
        public ICollection<TextEntryQuestionDTO> TextEntryQuestions { get; set; } = new List<TextEntryQuestionDTO>();
    }

    public class QuizWithAnswersDTO
    {
        public Guid Id { get; set; } = default!;
        
        public string Title { get; set; } = default!;
        
        public ICollection<TextEntryQuestionWithAnswersDTO> TextEntryQuestions { get; set; } = new List<TextEntryQuestionWithAnswersDTO>();
    }

    public class QuizDTOSummary
    {
        public Guid Id { get; set; } = default!;
        
        public string Title { get; set; } = default!;

        public int QuestionCount { get; set; }
    }

    public class CreateQuizDTO
    {
        [MinLength(1)]
        [MaxLength(512)]
        [Required]
        public string Title { get; set; } = default!;
    }
}
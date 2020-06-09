using System;
using System.ComponentModel.DataAnnotations;

namespace PublicApi.DTO
{
    public class TextEntryAnswerDTO
    {
        // public Guid AppUserId { get; set; } = default!;
        
        // public Guid TextEntryQuestionId { get; set; } = default!;

        public Guid Id { get; set; } = default!;

        public string Answer { get; set; } = default!;
    }
    
    public class TextEntryAnswerDTOSummary
    {
        public Guid TextEntryQuestionId { get; set; } = default!;

        public Guid Id { get; set; } = default!;

        public string Answer { get; set; } = default!;
    }

    public class CreateTextEntryAnswerDTO
    {
        public Guid AppUserId { get; set; } = default!;

        public Guid TextEntryQuestionId { get; set; } = default!;

        [MinLength(1)]
        [MaxLength(16384)]
        [Required]
        public string Answer { get; set; } = default!;
    }
}
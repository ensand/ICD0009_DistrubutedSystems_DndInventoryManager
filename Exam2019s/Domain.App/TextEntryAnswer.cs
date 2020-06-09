using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Domain.App.Identity;

namespace Domain.App
{
    public class TextEntryAnswer
    {
        [JsonIgnore]
        public AppUser? AppUser { get; set; }
        public Guid AppUserId { get; set; } = default!;
        // USe nickname instead? No need for an account then
        
        public Guid Id { get; set; } = default!;

        [JsonIgnore]
        public TextEntryQuestion? TextEntryQuestion { get; set; }
        public Guid TextEntryQuestionId { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(16384)]
        [Required]
        public string Answer { get; set; } = default!;
    }
}
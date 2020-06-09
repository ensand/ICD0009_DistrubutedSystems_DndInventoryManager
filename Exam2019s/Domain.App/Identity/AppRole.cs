using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppRole : IdentityRole<Guid>
    {
        [MinLength(1)]
        [MaxLength(128)]
        [Required]
        public string DisplayName { get; set; } = default!;
    }
}
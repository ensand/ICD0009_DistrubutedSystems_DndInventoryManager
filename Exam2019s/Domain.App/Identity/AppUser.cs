using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.App.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string Nickname { get; set; } = default!;
    }
}
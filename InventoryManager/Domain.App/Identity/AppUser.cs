using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : AppUser<Guid>
    {
    }

    public class AppUser<TKey> : IdentityUser<TKey> 
        where TKey : IEquatable<TKey>
    {
        [MinLength(1)] 
        [MaxLength(64)]
        [Required]
        public string FirstName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        [Required]
        public string LastName { get; set; } = default!;
    }
}
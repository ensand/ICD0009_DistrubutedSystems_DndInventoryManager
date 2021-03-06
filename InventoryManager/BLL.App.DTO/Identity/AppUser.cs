using System;
using System.ComponentModel.DataAnnotations;
using com.enola.inventorymanager.Contracts.Domain;

namespace BLL.App.DTO.Identity
{
    public class AppUser : IDomainEntityId 
    {
        public virtual Guid Id { get; set; } = default!;

        public string Email { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(64)]
        [Required]
        public virtual string FirstName { get; set; } = default!;
        
        [MinLength(1)] 
        [MaxLength(64)] 
        [Required]
        public virtual string LastName { get; set; } = default!;
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;

namespace Domain.Identity
{
    public class AppUser : IdentityUser<Guid>
    {
        [MinLength(1)] 
        [MaxLength(64)] 
        public string FirstName { get; set; } = default!;
        
        [MinLength(1)]
        [MaxLength(64)]
        public string LastName { get; set; } = default!;

        public ICollection<DndCharacter>? Characters { get; set; }
        public ICollection<Armor>? BaseArmors { get; set; }
        public ICollection<Weapon>? BaseWeapons { get; set; }
    }
}
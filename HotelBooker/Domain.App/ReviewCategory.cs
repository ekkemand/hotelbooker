using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class ReviewCategory : DomainEntityIdMetadata
    {
        [MaxLength(200)] public string Name { get; set; } = default!;
        public bool Accepted { get; set; } = false;
        
        public ICollection<Review>? Reviews { get; set; }
    }
}
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class OwnerCompany : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(100), Required] public string Name { get; set; } = default!;

        public ICollection<Hotel>? Hotels { get; set; }
    }
}
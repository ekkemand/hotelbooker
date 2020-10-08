using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class Currency : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(80), Required] public string Name { get; set; } = default!;
        public ICollection<Price>? Prices { get; set; }
    }
}
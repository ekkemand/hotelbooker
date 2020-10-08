using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ee.itcollege.ekmand.Domain.Base;

namespace Domain.App
{
    public class ConvenienceGroup : DomainEntityIdMetadata
    {
        [MinLength(1), MaxLength(80), Required] public string Name { get; set; } = default!;
        [MaxLength(2000)] public string? Description { get; set; }

        public ICollection<RoomTypeConvenience>? RoomTypeConveniences { get; set; }
        public ICollection<Convenience>? Conveniences { get; set; }
    }
}
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class GroupedConvenienceDTO
    {
        public ConvenienceGroup ConvenienceGroup { get; set; } = default!;

        public IEnumerable<Convenience>? Conveniences { get; set; }
    }
}
using System.Collections.Generic;

namespace BLL.App.DTO.HelperClasses
{
    public class GroupedConvenience
    {
        public ConvenienceGroup ConvenienceGroup { get; set; } = default!;

        public IEnumerable<Convenience>? Conveniences { get; set; }
    }
}
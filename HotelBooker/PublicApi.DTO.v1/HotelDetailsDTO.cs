
using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class HotelDetailsDTO
    {
        public Hotel Hotel { get; set; } = default!;
        public IEnumerable<GroupedConvenienceDTO>? GroupedConveniences { get; set; }
        public IEnumerable<Review>? Reviews { get; set; }
        
    }
}
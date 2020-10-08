using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class HotelIndexData
    {
        public IEnumerable<Hotel> Hotels { get; set; } = default!;
        
        public HotelFilterData? FilterData { get; set; }

        public FiltersSelectionsDTO? Selections { get; set; }
    }
}
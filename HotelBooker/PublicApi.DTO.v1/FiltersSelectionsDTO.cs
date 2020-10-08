using System.Collections.Generic;

namespace PublicApi.DTO.v1
{
    public class FiltersSelectionsDTO
    {
        public IEnumerable<OwnerCompany>? OwnerCompanySelection { get; set; }
        public IEnumerable<Convenience>? ConvenienceSelection { get; set; }
        public IEnumerable<ReviewCategory>? ReviewCategorySelection { get; set; }
        public IEnumerable<string>? AlphabeticalOrderOptions { get; set; }
        public IEnumerable<string>? EstablishedDateOrderOptions { get; set; }
    }
}
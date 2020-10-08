using System.Collections.Generic;
using System.Linq;

#pragma warning disable 1591
namespace HotelBooker.Helpers
{
    public class GeneralOrderingOptions
    {
        public Dictionary<OrderOption, string> Options { get; set; }
        public List<string> OptionsForView { get; set; }

        public GeneralOrderingOptions()
        {
            Options = new Dictionary<OrderOption, string>
            {
                {OrderOption.Default, "Choose"},
                {OrderOption.Ascending, "Ascending"}, 
                {OrderOption.Descending, "Descending"}
            };
            OptionsForView = Options.Values.ToList();
        }
    }
    
    public enum OrderOption
    {
        Default,
        Ascending,
        Descending
    }
}
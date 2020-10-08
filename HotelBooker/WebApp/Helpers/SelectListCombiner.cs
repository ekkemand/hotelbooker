#pragma warning disable 1591

using System;
using System.Collections.Generic;
using System.Linq;
using BLL.App.DTO;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HotelBooker.Helpers
{
    public class SelectListCombiner
    {
        public SelectList NullableOwnersSelectList(IEnumerable<OwnerCompany> owners)
        {
            var result = new List<SelectListItem>
            {
                new SelectListItem {Text = "Choose...", Value = Guid.Empty.ToString(), Selected = true}
            };

            foreach (var item in owners)
            {
                result.Add(new SelectListItem
                { 
                    Text = item.Name, 
                    Value = item.Id.ToString()
                });
            }

            return new SelectList(result, "Value", "Text");
        }
        
        public SelectList NullableConveniencesSelectList(IEnumerable<Convenience> conveniences)
        {
            var result = new List<SelectListItem>
            {
                new SelectListItem {Text = "Choose...", Value = Guid.Empty.ToString(), Selected = true}
            };
            result.AddRange(conveniences.Select(convenience => new SelectListItem
            { 
                Text = convenience.Name, 
                Value = convenience.Id.ToString()
            }));

            return new SelectList(result, "Value", "Text");
        }
        
        public SelectList NullableReviewCategoriesSelectList(IEnumerable<ReviewCategory> reviewCategories)
        {
            var result = new List<SelectListItem>
            {
                new SelectListItem {Text = "Choose...", Value = Guid.Empty.ToString(), Selected = true}
            };
            result.AddRange(reviewCategories.Select(item => new SelectListItem
            { 
                Text = item.Name, 
                Value = item.Id.ToString()
            }));

            return new SelectList(result, "Value", "Text");
        }
        
        public SelectList NullableRoomTypesSelectList(IEnumerable<RoomType> roomTypes)
        {
            var result = new List<SelectListItem>
            {
                new SelectListItem {Text = "Choose...", Value = Guid.Empty.ToString(), Selected = true}
            };
            result.AddRange(roomTypes.Select(item => new SelectListItem
            { 
                Text = item.Type, 
                Value = item.Id.ToString()
            }));

            return new SelectList(result, "Value", "Text");
        }
        
        public SelectList NullableCampaignList(IEnumerable<Campaign> campaigns)
        {
            var result = new List<SelectListItem>
            {
                new SelectListItem {Text = "Choose...", Value = Guid.Empty.ToString(), Selected = true}
            };

            result.AddRange(campaigns.Select(item => new SelectListItem
            { 
                Text = item.Name, 
                Value = item.Id.ToString()
            }));

            return new SelectList(result, "Value", "Text");
        }
    }
}
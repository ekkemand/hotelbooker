using BLL.App.Mappers;
using ee.itcollege.ekmand.BLL.Base.Services;
using Contracts.BLL.App.Mappers;
using Contracts.BLL.App.Services;
using Contracts.DAL.App;
using Contracts.DAL.App.Repositories;


namespace BLL.App.Services
{
    public class CampaignService : BaseEntityService<IAppUnitOfWork, ICampaignRepository, ICampaignServiceMapper,
        DAL.App.DTO.Campaign, BLL.App.DTO.Campaign>, ICampaignService
    {
        public CampaignService(IAppUnitOfWork unitOfWork)
            : base(unitOfWork, unitOfWork.Campaigns, new CampaignServiceMapper())
        {
        }
        // public override async Task<Campaign> RemoveAsync(Campaign entity, object? userId = null)
        // {
        //     await DeleteChildEntities(entity.Id);
        //     return await base.RemoveAsync(entity, userId);
        // }
        //
        // public override async Task<Campaign> RemoveAsync(Guid id, object? userId = null)
        // {
        //     await DeleteChildEntities(id);
        //     return await base.RemoveAsync(id, userId);
        // }
        //
        // private async Task<IEnumerable<Price>> DeleteChildEntities(Guid campaignId)
        // {
        //     var children = (await UnitOfWork.Prices.GetAllAsync())
        //         .Where(o => o.CampaignId == campaignId);
        //     foreach (var child in children)
        //     {
        //         await UnitOfWork.Prices.RemoveAsync(child);
        //     }
        //     return new List<Price>();
        // }
    }
}
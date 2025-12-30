using System;
using System.Collections.Generic;
using System.Text;
using TabletopNote.Core.Models;
using TabletopNote.Data.Entities;

namespace TabletopNote.Data.Mappings
{
    public static class CampaignMapper
    {
        public static Campaign ToDomain(this CampaignEntity entity)
            => new()
            {
                CampaignId = entity.CampaignId,
                CampaignName = entity.CampaignName,
                CampaignDescription = entity.CampaignDescription
            };

        public static CampaignEntity ToEntity(this Campaign domain, int campaignId)
            => new()
            {
                CampaignId = domain.CampaignId,
                CampaignName = domain.CampaignName,
                CampaignDescription = domain.CampaignDescription
            };
    }
}

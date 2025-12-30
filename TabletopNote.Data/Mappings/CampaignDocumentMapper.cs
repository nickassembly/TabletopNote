using System;
using System.Collections.Generic;
using System.Text;
using TabletopNote.Core.Models;
using TabletopNote.Data.Entities;

namespace TabletopNote.Data.Mappings
{
    public static class CampaignDocumentMapper
    {
        public static CampaignDocument ToDomain(this CampaignDocumentEntity entity)
            => new()
            {
                DocumentId = entity.DocumentId,
                DocumentName = entity.DocumentName,
                DocumentDescription = entity.DocumentDescription,
                DocumentContentType = (DocumentContentType)entity.DocumentContentType,
                DocumentContent = entity.DocumentContent,
                IsGMVisibleOnly = entity.IsGMVisibleOnly,
                DocumentCreatedAt = entity.DocumentCreatedAt,
                DocumentUpdatedAt = entity.DocumentUpdatedAt
            };

        public static CampaignDocumentEntity ToEntity(this CampaignDocument domain, int campaignId)
            => new()
            {
                DocumentId = domain.DocumentId,
                CampaignId = campaignId,
                DocumentName = domain.DocumentName,
                DocumentDescription = domain.DocumentDescription,
                DocumentContentType = (int)domain.DocumentContentType,
                DocumentContent = domain.DocumentContent,
                IsGMVisibleOnly = domain.IsGMVisibleOnly,
                DocumentCreatedAt = domain.DocumentCreatedAt,
                DocumentUpdatedAt = domain.DocumentUpdatedAt
            };
    }
}

using TabletopNote.Core.Models;
using TabletopNote.Data.Entities;

namespace TabletopNote.Data.Mappings
{
    public static class ReferenceDocumentMapper
    {
        public static ReferenceDocument ToDomain(this ReferenceDocumentEntity entity)
            => new()
            {
                FileId = entity.FileId,
                ReferenceFileName = entity.ReferenceFileName,
                FileDescription = entity.FileDescription,
                FilePath = entity.FilePath,
                Url = entity.Url,
                IsGMVisibleOnly = entity.IsGMVisibleOnly,
            };

        public static ReferenceDocumentEntity ToEntity(this ReferenceDocument domain, int campaignId)
            => new()
            {
                FileId = domain.FileId,
                CampaignId = campaignId,
                ReferenceFileName = domain.ReferenceFileName,
                FileDescription = domain.FileDescription,
                FilePath = domain.FilePath,
                Url = domain.Url,
                IsGMVisibleOnly = domain.IsGMVisibleOnly,
            };
    }
}

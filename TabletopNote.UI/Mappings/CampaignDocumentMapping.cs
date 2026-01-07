using TabletopNote.API.Dtos;
using TabletopNote.UI.ViewModels;

namespace TabletopNote.UI.Mappings
{
    public static class CampaignDocumentMappings
    {
        public static CampaignDocumentVM ToViewModel(
            this CampaignDocumentDto dto)
        {
            return new CampaignDocumentVM
            {
                DocumentId = dto.DocumentId,
                DocumentName = dto.DocumentName,
                DocumentDescription = dto.DocumentDescription,
                DocumentContent = dto.DocumentContent,
                DocumentContentType = dto.DocumentContentType,
                DocumentCreatedAt = dto.DocumentCreatedAt,
                DocumentUpdatedAt = dto.DocumentUpdatedAt,
                IsGMVisibleOnly = dto.IsGMVisibleOnly
            };
        }
    }
}

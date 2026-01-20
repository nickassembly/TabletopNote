
namespace TabletopNote.Data.Entities
{
    public class CampaignDocumentEntity
    {
        public int DocumentId { get; set; }
        public int CampaignId { get; set; }

        public string DocumentName { get; set; } = string.Empty;
        public string? DocumentDescription { get; set; }
        public int DocumentContentType { get; set; }

        public string? DocumentContent { get; set; }
        public bool IsGMVisibleOnly { get; set; }

        public DateTime DocumentCreatedAt { get; set; }
        public DateTime DocumentUpdatedAt { get; set; }

        public CampaignEntity Campaign { get; set; } = null!;
    }
}

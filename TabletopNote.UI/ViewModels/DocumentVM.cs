using TabletopNote.Core.Models;

namespace TabletopNote.UI.ViewModels
{
    public class DocumentVM
    {
        public int? DocumentId { get; set; }
        public int CampaignId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentDescription { get; set; } = string.Empty;
        public DocumentContentType DocumentContentType { get; set; }
        public string? DocumentContent { get; set; }
        public bool IsGMVisibleOnly { get; set; }
        public DateTime DocumentCreatedAt { get; set; }
        public DateTime DocumentUpdatedAt { get; set; }
    }
}

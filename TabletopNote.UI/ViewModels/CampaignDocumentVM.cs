using TabletopNote.Core.Models;

namespace TabletopNote.UI.ViewModels
{
    public class CampaignDocumentVM
    {
        public int DocumentId { get; set; }
        public string DocumentName { get; set; } = string.Empty;
        public string DocumentDescription { get; set; } = string.Empty;
        public DocumentContentType DocumentContentType { get; set; }
        public string? DocumentContent { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}

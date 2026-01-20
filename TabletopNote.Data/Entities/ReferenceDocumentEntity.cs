namespace TabletopNote.Data.Entities
{
    public class ReferenceDocumentEntity
    {
        public int FileId { get; set; }
        public int CampaignId { get; set; }
        public string ReferenceFileName { get; set; } = string.Empty;
        public string? FileDescription { get; set; }
        public string? FilePath { get; set; }
        public string? Url { get; set; }
        public bool IsGMVisibleOnly { get; set; }

        public CampaignEntity Campaign { get; set; } = null!;
    }
}

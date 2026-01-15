namespace TabletopNote.UI.ViewModels
{
    public class ReferenceVM
    {
        public int CampaignId { get; set; }
        public int FileId { get; set; }
        public string ReferenceFileName { get; set; } = string.Empty;
        public string FileDescription { get; set; } = string.Empty;
        public string? FilePath { get; set; }
        public string? Url { get; set; }
    }
}

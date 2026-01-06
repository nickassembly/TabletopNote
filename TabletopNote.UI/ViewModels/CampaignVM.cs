namespace TabletopNote.UI.ViewModels
{
    public class CampaignVM
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string CampaignDescription { get; set; } = string.Empty;
        public List<CampaignDocumentDto> CampaignDocuments { get; set; } = new();
        public List<ReferenceDocumentDto> ReferenceDocuments { get; set; } = new();
        public List<CalendarEventDto> CalendarEvents { get; set; } = new();
    }
}

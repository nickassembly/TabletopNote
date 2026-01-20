namespace TabletopNote.Data.Entities
{
    public class CampaignEntity
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string? CampaignDescription { get; set; }

        public ICollection<CampaignDocumentEntity> CampaignDocuments { get; set; } = new List<CampaignDocumentEntity>();
        public ICollection<ReferenceDocumentEntity> ReferenceDocuments { get; set; } = new List<ReferenceDocumentEntity>();
        public ICollection<CalendarEventEntity> CalendarEvents { get; set; } = new List<CalendarEventEntity>();
    }
}

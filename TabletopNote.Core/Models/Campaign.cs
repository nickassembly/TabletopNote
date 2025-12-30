using System.Reflection.Metadata;

namespace TabletopNote.Core.Models
{
    public class Campaign
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public string CampaignDescription { get; set; } = string.Empty;
        public List<CampaignDocument> CampaignDocuments { get; set; } = new();
        public List<ReferenceDocument> ReferenceDocuments { get; set; } = new();
        public List<CalendarEvent> CalendarEvents { get; set; } = new();
    }
}

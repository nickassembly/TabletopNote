namespace TabletopNote.Shared.Dto
{
    public class EventsByCampaignDto
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }  = string.Empty;
        public List<CalendarEventDto> CalendarEvents { get; set; } = [];
    }
}

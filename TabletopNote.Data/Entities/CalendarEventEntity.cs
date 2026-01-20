namespace TabletopNote.Data.Entities
{
    public class CalendarEventEntity
    {
        public int CalendarEventId { get; set; }
        public int CampaignId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string? EventDescription { get; set; }
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool IsGMVisibleOnly { get; set; }

        public CampaignEntity Campaign { get; set; } = null!;
    }
}

namespace TabletopNote.UI.ViewModels
{
    public class CalendarEventVM
    {
        public int CampaignId { get; set; }
        public int CalendarEventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public DateTime? EventStartDate { get; set; }
        public DateTime? EventEndDate { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}

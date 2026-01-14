namespace TabletopNote.Shared.Dto
{
    public class CalendarEventDto
    {
        public int CalendarEventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace TabletopNote.Core.Models
{
    public class CalendarEvent
    {
        public int CalendarEventId { get; set; }
        public string EventName { get; set; } = string.Empty;
        public string EventDescription { get; set; } = string.Empty;
        public DateTime EventStartDate { get; set; }
        public DateTime EventEndDate { get; set; }
        public bool IsGMVisibleOnly { get; set; }
    }
}

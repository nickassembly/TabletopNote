using TabletopNote.Core.Models;
using TabletopNote.Data.Entities;

namespace TabletopNote.Data.Mappings
{
    public static class CalendarEventMapper
    {
        public static CalendarEvent ToDomain(this CalendarEventEntity entity)
            => new()
            {
                CalendarEventId = entity.CalendarEventId,
                EventName = entity.EventName,
                EventDescription = entity.EventDescription,
                EventStartDate = entity.EventStartDate,
                EventEndDate = entity.EventEndDate,
                IsGMVisibleOnly = entity.IsGMVisibleOnly,
            };

        public static CalendarEventEntity ToEntity(this CalendarEvent domain, int campaignId)
            => new()
            {
                CalendarEventId = domain.CalendarEventId,
                CampaignId = campaignId,
                EventName = domain.EventName,
                EventDescription = domain.EventDescription,
                EventStartDate  = domain.EventStartDate,
                EventEndDate = domain.EventEndDate,
                IsGMVisibleOnly = domain.IsGMVisibleOnly,
            };
    }
}

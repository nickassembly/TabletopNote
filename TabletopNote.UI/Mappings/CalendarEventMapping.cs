using TabletopNote.Shared.Dto;
using TabletopNote.UI.ViewModels;

namespace TabletopNote.UI.Mappings
{
    public static class CalendarEventMapping
    {
        public static CalendarEventVM ToViewModel(
            this CalendarEventDto dto)
        {
            return new CalendarEventVM
            {
                CampaignId = dto.CampaignId,
                CalendarEventId = dto.CalendarEventId,
                EventName = dto.EventName,
                EventDescription = dto.EventDescription,
                EventStartDate = dto.EventStartDate,
                EventEndDate = dto.EventEndDate,
                IsGMVisibleOnly = dto.IsGMVisibleOnly
            };
        }
    }
}

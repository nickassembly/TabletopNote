
namespace TabletopNote.UI.ViewModels
{
    public class EventsByCampaignVM
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public List<CalendarEventVM> CalendarEvents { get; set; } = [];
    }
}

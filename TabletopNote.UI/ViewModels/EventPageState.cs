namespace TabletopNote.UI.ViewModels
{
    public class EventPageState
    {
        public EventsByCampaignVM? Page { get; set; }
        public bool IsLoading { get; set; }
        public string? Error { get; set; }
    }
}

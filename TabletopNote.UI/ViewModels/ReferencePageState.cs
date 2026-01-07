namespace TabletopNote.UI.ViewModels
{
    public class ReferencePageState
    {
        public ReferencesByCampaignVM? Page { get; set; }
        public bool IsLoading { get; set; }
        public string? Error { get; set; }
    }
}

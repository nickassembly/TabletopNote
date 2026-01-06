namespace TabletopNote.UI.ViewModels
{
    public class CampaignPageState
    {
        public List<CampaignVM> Page { get; set; } = [];
        public bool IsLoading { get; set; }
        public string? Error { get; set; }
    }
}

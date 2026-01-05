namespace TabletopNote.UI.ViewModels
{
    public class DocumentPageState
    {
        public DocumentsByCampaignVM? Page { get; set; }
        public bool IsLoading { get; set; }
        public string? Error { get; set; }
    }
}

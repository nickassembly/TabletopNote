
namespace TabletopNote.UI.ViewModels
{
    public class ReferencesByCampaignVM
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public List<ReferenceDocumentVM> ReferenceDocuments { get; set; } = [];
    }
}

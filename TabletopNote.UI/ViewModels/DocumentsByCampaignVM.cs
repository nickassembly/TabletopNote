
namespace TabletopNote.UI.ViewModels
{
    public class DocumentsByCampaignVM
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; } = string.Empty;
        public List<DocumentVM> CampaignDocuments { get; set; } = [];
    }
}

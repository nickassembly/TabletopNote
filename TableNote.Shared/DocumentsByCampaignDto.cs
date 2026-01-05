
namespace TabletopNote.Shared
{
    public class DocumentsByCampaignDto
    {
        public int CampaignId { get; set; }
        public string CampaignName { get; set; }  = string.Empty;
        public List<CampaignDocumentDto> CampaignDocuments { get; set; } = [];
    }
}


public class ReferencesByCampaignDto
{
    public int CampaignId { get; set; }
    public string CampaignName { get; set; } = string.Empty;
    public List<ReferenceDocumentDto> ReferenceDocuments { get; set; } = [];
}


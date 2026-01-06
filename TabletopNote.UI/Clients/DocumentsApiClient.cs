using TabletopNote.Shared;

namespace TabletopNote.UI.Clients
{
    public class DocumentsApiClient
    {
        private readonly HttpClient _http;

        public DocumentsApiClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<List<CampaignDto>> GetAllCampaigns()
        {
            return await _http.GetFromJsonAsync<List<CampaignDto>>(
                $"/campaigns"
            ) ?? throw new InvalidOperationException("No response");
        }

        public async Task<CampaignDto> GetCampaignById(int campaignId)
        {
            return await _http.GetFromJsonAsync<CampaignDto>(
                $"/campaigns/{campaignId}"
            ) ?? throw new InvalidOperationException("No response");
        }

        public async Task<DocumentsByCampaignDto> GetAllCampaignDocuments(int campaignId)
        {
            return await _http.GetFromJsonAsync<DocumentsByCampaignDto>(
                $"campaigns/{campaignId}/documents"
            ) ?? throw new InvalidOperationException("No response");
        }
    }
}

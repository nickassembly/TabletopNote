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

        public async Task<DocumentsByCampaignDto> GetAllCampaignDocuments(int campaignId)
        {
            var test = await _http.GetFromJsonAsync<DocumentsByCampaignDto>($"campaigns/{campaignId}/documents");
            //return await _http.GetFromJsonAsync<DocumentsByCampaignDto>(
            //    $"campaigns/{campaignId}/documents"
            //) ?? throw new InvalidOperationException("No response");

                return test;
        }
    }
}

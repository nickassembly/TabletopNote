using TabletopNote.Shared.Dto;

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

        public async Task<CampaignDto> AddCampaign(CampaignAddDto campaignToAdd)
        {
            var response = await _http.PostAsJsonAsync(
                $"/campaigns", campaignToAdd
            );

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to add new campaign");

            var campaign = await response.Content.ReadFromJsonAsync<CampaignDto>();

            return campaign 
                ?? throw new InvalidOperationException("No campaign returned");
        }

        public async Task UpdateCampaign(CampaignUpdateDto campaignToUpdate, int campaignId)
        {
            var response = await _http.PutAsJsonAsync(
                $"/campaigns/{campaignId}", campaignToUpdate
            );

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to update campaign {campaignId}");
        }

        public async Task<CampaignDto> GetCampaignById(int campaignId)
        {
            return await _http.GetFromJsonAsync<CampaignDto>(
                $"/campaigns/{campaignId}"
            ) ?? throw new InvalidOperationException("No response");
        }

        public async Task DeleteCampaign(int campaignId)
        {
            var response = await _http.DeleteAsync(
                $"/campaigns/{campaignId}"
            );

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
        }

        public async Task<DocumentsByCampaignDto> GetAllCampaignDocuments(int campaignId)
        {
            return await _http.GetFromJsonAsync<DocumentsByCampaignDto>(
                $"campaigns/{campaignId}/documents"
            ) ?? throw new InvalidOperationException("No response");
        }

        public async Task<CampaignDocumentDto> GetCampaignDocumentById(int campaignId, int documentId)
        {
            return await _http.GetFromJsonAsync<CampaignDocumentDto>(
                $"/campaigns/{campaignId}/documents/{documentId}"
            ) ?? throw new InvalidOperationException("No response");
        }

        public async Task<CampaignDocumentDto> AddCampaignDocument(int campaignId, CampaignDocumentAddDto campaignDocumentToAdd)
        {
            var response = await _http.PostAsJsonAsync(
               $"/campaigns/{campaignId}/documents", campaignDocumentToAdd
           );

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to add new campaign");

            var document = await response.Content.ReadFromJsonAsync<CampaignDocumentDto>();

            return document
                ?? throw new InvalidOperationException("No campaign document returned");
        }

        public async Task UpdateCampaignDocument(int campaignId, int documentId, CampaignDocumentUpdateDto documentToUpdate)
        {
            var response = await _http.PutAsJsonAsync(
                $"/campaigns/{campaignId}/documents/{documentId}", documentToUpdate
            );

            if (!response.IsSuccessStatusCode)
                throw new HttpRequestException($"Failed to update document: {documentToUpdate.DocumentName}");
        }

        public async Task DeleteCampaignDocument(int campaignId, int documentId)
        {
            var response = await _http.DeleteAsync(
                $"/campaigns/{campaignId}/documents/{documentId}"
            );

            if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                throw new KeyNotFoundException($"Campaign with ID {campaignId} not found.");
        }

        public async Task<ReferencesByCampaignDto> GetAllReferenceDocuments(int campaignId)
        {
            return await _http.GetFromJsonAsync<ReferencesByCampaignDto>(
                $"campaigns/{campaignId}/references"
            ) ?? throw new InvalidOperationException("No response");
        }

        public async Task<EventsByCampaignDto> GetAllCalendarEvents(int campaignId)
        {
            return await _http.GetFromJsonAsync<EventsByCampaignDto>(
                $"campaigns/{campaignId}/events"
            ) ?? throw new InvalidOperationException("No response");
        }
    }
}

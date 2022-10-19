using DevelopersAPI.Controllers;
using DevelopersAPI.Models;

namespace DevelopersApi.Adapters;

public class OutageSupplierHttpAdapter
{
    private readonly HttpClient _httpClient;

    public OutageSupplierHttpAdapter(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }



    public async Task<List<ScheduledOutage>?> GetScheduledOutagesAsync()
    {
        var response = await _httpClient.GetAsync("/services/developers/outages");

        response.EnsureSuccessStatusCode();

        var outages = await response.Content.ReadFromJsonAsync<CollectionResponse<ScheduledOutage>>();
        if (outages != null)
        {
            return outages.Data;
        }
        return null;
    }
}
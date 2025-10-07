using System.Net.Http.Json;
using System.Text.Json.Serialization;
using TripPlanner.Application.Common.Interfaces;

namespace TripPlanner.Infrastructure.Services;

public class NominatimOsmPlacesService(HttpClient http) : IPlacesService
{

    private const string UserAgent = "TripPlanner/1.0";

    private record NominatimResult
    {
        [JsonPropertyName("place_id")]
        public long PlaceId { get; set; }
        [JsonPropertyName("display_name")]
        public string DisplayName { get; set; } = string.Empty;
    }

    public async Task<List<IPlacesService.PlaceSuggestion>> SearchAsync(string query, CancellationToken cancellationToken = default)
    {
        var results = new List<IPlacesService.PlaceSuggestion>();
        if (string.IsNullOrWhiteSpace(query)) return results;

        // Nominatim public API requires a User-Agent and provides display_name we can use
        using var request = new HttpRequestMessage(HttpMethod.Get, $"https://nominatim.openstreetmap.org/search?format=json&q={Uri.EscapeDataString(query)}");
        request.Headers.TryAddWithoutValidation("User-Agent", UserAgent);

        using var response = await http.SendAsync(request, cancellationToken);
        response.EnsureSuccessStatusCode();

        var data = await response.Content.ReadFromJsonAsync<List<NominatimResult>>(cancellationToken: cancellationToken) 
                   ?? [];

        foreach (var item in data.Take(5))
        {
            // For simplicity, split display_name into name and address (name, address...)
            var parts = item.DisplayName.Split(',', 2, StringSplitOptions.TrimEntries);
            var name = parts.Length > 0 ? parts[0] : item.DisplayName;
            var address = parts.Length > 1 ? parts[1] : null;
            results.Add(new IPlacesService.PlaceSuggestion(item.PlaceId.ToString(), name, address));
        }

        return results;
    }
}

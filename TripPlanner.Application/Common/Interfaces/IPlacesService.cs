namespace TripPlanner.Application.Common.Interfaces;

public interface IPlacesService
{
    public record PlaceSuggestion(string Id, string DisplayName, string? Address);
    public Task<List<PlaceSuggestion>> SearchAsync(string query, CancellationToken cancellationToken = default);
}
namespace TripPlanner.Application.Common.Interfaces;

public interface IUser
{
    string? Id { get; }
    void SetAccessKey(string key);
}
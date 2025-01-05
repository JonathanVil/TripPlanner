namespace TripPlanner.Application.Common.Interfaces;

public interface IFileProvider
{
    public string GetBasePath();
    public Task<string> SaveFileAsync(Stream fileStream, string fileName, string folderName);
    public Task<bool> DeleteFileAsync(string fileName, string folderName);
}
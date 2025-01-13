using CrypticWizard.RandomWordGenerator;
using TripPlanner.Application.Common.Interfaces;
using TripPlanner.Application.Common.Models;
using TripPlanner.Core.Entities;
using TripPlanner.Infrastructure.Data;

namespace TripPlanner.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly ApplicationDbContext _context;

    public IdentityService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<string?> GetUserNameAsync(string userId)
    {
        var user = await _context.Users.FindAsync(userId);
        return user?.Name;
    }
    
    public string? GetUserName(string userId)
    {
        var user = _context.Users.Find(userId);
        return user?.Name;
    }

    private string GenerateAccessKey()
    {
        string phrase;
        do
        {
            phrase = string.Join('-', GenerateRandomPhrase());
        } while (_context.Users.Any(u => u.Id == phrase.GetSha256Hash()));
        
        return phrase;
    }

    private static List<string> GenerateRandomPhrase()
    {
        var generator = new WordGenerator();
        return generator.GetWords(WordGenerator.PartOfSpeech.noun, 12);
    }

    public async Task<(Result Result, string AccessKey)> CreateUserAsync(string name)
    {
        var accessKey = GenerateAccessKey();
        var hash = accessKey.GetSha256Hash();

        var user = new User { Name = name, Id = hash };
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return (Result.Success(), accessKey);
    }

    public async Task<Result> DeleteUserAsync(Guid userId)
    {
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
        {
            return Result.Failure(["User not found."]);
        }

        await DeleteUserAsync(user);

        return Result.Success();
    }

    public async Task<Result> DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return Result.Success();
    }
}
using Microsoft.EntityFrameworkCore;
namespace MusicPlatform;
public class Service
{
    private readonly DbContext _context;
    public Service(DbContext context)
    {
        _context = context;
    }

    public async Task SetFavoriteGenres()
    {
        var users = _context.Set<User>().ToList();
        var allListenings = _context.Set<Listening>().ToList();
        var genres = _context.Set<Genre>().Include(g => g.Links).ToList();
        foreach (var user in users)
        {
            var userListenings = allListenings.Where(l => l.UserId == user.Id);
            var topGenreIds = userListenings
                .SelectMany(h => genres.Where(g => g.Links.Any(l => l.AuthorId == h.AuthorId)))
                .GroupBy(g => g.Id)
                .OrderByDescending(group => group.Count())
                .Take(3)
                .Select(group => group.Key)
                .ToList();

            user.FavoriteGenres = _context.Set<Genre>().Where(g => topGenreIds.Contains(g.Id)).ToList();

            await _context.SaveChangesAsync();
        }
    }
}
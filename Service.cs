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
        var users = _context.Set<User>().Include(u => u.History).ToList();
        var genreLinks = _context.Set<GenreLink>().ToList();
        foreach (var user in users)
        {
            var genres = user.History
                .SelectMany(h => genreLinks.Where(gl => gl.AuthorId == h.Id))
                .GroupBy(g => g.GenreId)
                .OrderByDescending(group => group.Count())
                .Take(3)
                .Select(group => group.Key)
                .ToList();

            user.FavoriteGenres = _context.Set<Genre>().Where(g => genres.Contains(g.Id)).ToList();

            await _context.SaveChangesAsync();
        }
    }
}
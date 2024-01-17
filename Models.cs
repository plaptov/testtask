namespace MusicPlatform;
public class User
{
    public long Id { get; set; }
    public string Name { get; set; }
    public List<Listening> History { get; set; }
    public List<Genre> FavoriteGenres { get; set; }
}
public class Author
{
    public long Id { get; set; }
    public string Name { get; set; }
}
public class Listening
{
    public long Id { get; set; }
    public long UserId { get; set; }
    public long AuthorId { get; set; }
}
public class Genre
{
    public long Id { get; set; }
    public string Name { get; set; }
}
public class GenreLink
{
    public long AuthorId { get; set; }
    public long GenreId { get; set; }
}

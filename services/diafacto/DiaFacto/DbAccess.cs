using DiaFacto.Models;
using StackExchange.Redis;

namespace DiaFacto;

public class DbAccess
{
    private readonly ILogger<DbAccess> _logger;
    private readonly IDatabase _redis;
    private readonly IServer _server;

    public DbAccess(ILogger<DbAccess> logger, IConnectionMultiplexer multiplexer)
    {
        _logger = logger;
        _redis = multiplexer.GetDatabase();
        _server = multiplexer.GetServer(multiplexer.GetEndPoints()[0]);
    }

    private static DateTime GetDateTime(string value)
    {
        var millis = long.Parse(value);
        var dateTime = DateTimeOffset.FromUnixTimeMilliseconds(millis).DateTime;
        return dateTime;
    }


    public Subject? GetSubject(string id)
    {
        var title = _redis.StringGet($"subject:{id}:title");
        var fullName = _redis.StringGet($"subject:{id}:full_name");
        var teacher = _redis.StringGet($"subject:{id}:teacher");
        var info = _redis.StringGet($"subject:{id}:info");
        var platform = _redis.StringGet($"subject:{id}:platform");
        var link = _redis.StringGet($"subject:{id}:link");
        if (title.IsNullOrEmpty)
            return null;
        
        var subject = new Subject
        {
            Id = id,
            Title = title.ToString(),
            FullName = fullName.ToString(),
            Teacher = teacher.ToString(),
            Info = info.ToString(),
            Platform = platform.ToString(),
            Link = link.ToString()
        };
        return subject;
    }

    public IEnumerable<Subject?> GetSubjects()
    {
        var keyAll = _server.Keys(pattern: "subject:*:title");
        foreach (var key in keyAll)
            yield return GetSubject(key.ToString().Split(':')[1]);
    }
    public List<Subject> GetSubjectsList()
    {
        var subjects = GetSubjects();
        return subjects.Where(subject => subject is not null).Select(subject => subject!).ToList();
    }


    public User? GetUser(string id)
    {
        var name = _redis.StringGet($"user:{id}:name");
        var surname = _redis.StringGet($"user:{id}:surname");
        var avatarImage = _redis.StringGet($"user:{id}:avatar_image");
        var createdAt = _redis.StringGet($"user:{id}:created_at");
        var mails = _redis.SetMembers($"user:{id}:mails").ToStringArray();

        if (name.IsNullOrEmpty || createdAt.IsNullOrEmpty)
            return null;
        if (mails.Length == 0 || mails.Any(string.IsNullOrEmpty))
            return null;

        var user = new User
        {
            Id = id,
            Name = name.ToString(),
            Surname = surname.ToString(),
            AvatarImage = avatarImage.ToString(),
            CreatedAt = GetDateTime(createdAt.ToString()),
            Mails = mails!
        };
        return user;
    }

    public IEnumerable<User?> GetUsers()
    {
        var keyAll = _server.Keys(pattern: "user:*:name");
        foreach (var key in keyAll)
            yield return GetUser(key.ToString().Split(':')[1]);
    }
    public List<User> GetUsersList()
    {
        var users = GetUsers();
        return users.Where(user => user is not null).Select(user => user!).ToList();
    }
}
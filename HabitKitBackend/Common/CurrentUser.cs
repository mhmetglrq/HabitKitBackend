public interface ICurrentUser
{
    Guid UserId { get; }
}

internal sealed class HeaderCurrentUser(IHttpContextAccessor accessor) : ICurrentUser
{
    private static readonly Guid DevUserId = Guid.Parse("11111111-1111-1111-1111-111111111111");

    public Guid UserId
    {
        get
        {
            var header = accessor.HttpContext?.Request.Headers["X-User-Id"].FirstOrDefault();
            return header is not null && Guid.TryParse(header, out var userId) ? userId : DevUserId;
        }
    }
}
namespace PrintCenter.Infrastructure.Accessors
{
    public interface ICurrentUserIdentifier
    {
        string GetUserId();

        string GetUsername();

        string GetRole();
    }
}
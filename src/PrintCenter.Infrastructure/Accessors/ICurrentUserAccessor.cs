namespace PrintCenter.Infrastructure.Accessors
{
    public interface ICurrentUserAccessor
    {
        string GetUsername();

        string GetRole();
    }
}
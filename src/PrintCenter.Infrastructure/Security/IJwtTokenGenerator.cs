namespace PrintCenter.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        string CreateToken(string username, string role);
    }
}
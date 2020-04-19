namespace PrintCenter.Infrastructure.Security
{
    public interface IJwtTokenGenerator
    {
        string CreateToken(int userId, string username, string role);
    }
}
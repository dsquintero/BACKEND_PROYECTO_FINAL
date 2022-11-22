namespace API.Core.Interfaces
{
    public interface IPasswordHasherService
    {
        string SHA1(string password);
        bool VerifyPassword(string password, string passwordHash);
    }
}

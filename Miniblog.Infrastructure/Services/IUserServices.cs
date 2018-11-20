namespace Miniblog.Infrastructure.Services
{
    public interface IUserServices
    {
        bool ValidateUser(string username, string password);
    }
}

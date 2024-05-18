using WalletTopUp.Models;

namespace WalletTopUp.Repositories
{
    public interface IUserRepository
    {
        Task<UserDetails> GetUserById(int userId);

    }
}

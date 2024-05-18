using ExternalUserApi.Model;

namespace ExternalUserApi.Repo
{
    public interface IAccountRepository
    {
        Task<Account> GetByIdAsync(string accountId);
        Task<IEnumerable<Account>> GetByUserIdAsync(string userId);
        Task<TransactionResponse> DebitBalanceAsync(string accountId, decimal debitBalance);
    }

}   

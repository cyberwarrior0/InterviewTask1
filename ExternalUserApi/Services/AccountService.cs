using ExternalUserApi.Model;
using ExternalUserApi.Repo;

namespace ExternalUserApi.Service
{
    public interface IAccountService
    {
        Task<decimal> GetAccountBalanceAsync(string accountId);
        Task<TransactionResponse> DebitAccountBalanceAsync(string accountId, decimal debitBalance);
    }

    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<decimal> GetAccountBalanceAsync(string accountId)
        {
            try
            {
                var account= await _accountRepository.GetByIdAsync(accountId);
                if(account==null)
                {
                    return 0;
                }
                return account.Balance;
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        public async Task<TransactionResponse> DebitAccountBalanceAsync(string accountId, decimal debitBalance)
        {
            return await _accountRepository.DebitBalanceAsync(accountId, debitBalance);          
        }
    }

}

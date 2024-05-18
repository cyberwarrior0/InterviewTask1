using ExternalUserApi.Data;
using ExternalUserApi.Model;
using Microsoft.EntityFrameworkCore;
using System.Security.Principal;

namespace ExternalUserApi.Repo
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AppDbContext _context;

        public AccountRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Account> GetByIdAsync(string accountId)
        {
            return await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(string userId)
        {
            return await _context.Accounts.Where(a => a.AccountId == userId).ToListAsync();
        }

        public async Task<TransactionResponse> DebitBalanceAsync(string accountId, decimal debitAmount)
        {
            try
            {
                var account = await _context.Accounts.FirstOrDefaultAsync(a => a.AccountId == accountId);
                if (account != null)
                {
                    if (account.Balance >= debitAmount)
                    {
                        account.Balance -= debitAmount;
                        await _context.SaveChangesAsync();
                    }
                }
                var transactionResponse = new TransactionResponse()
                {
                    transactionId = Guid.NewGuid().ToString(),
                    updatedBalance = account.Balance,
                    timeStamp = DateTime.Now,
                    status = "success"
                };
                return transactionResponse;
            }
            catch (Exception ex)
            {
                var transactionResponse = new TransactionResponse()
                {
                    transactionId = Guid.NewGuid().ToString(),
                    timeStamp = DateTime.Now,
                    status = "success"
                };
                return transactionResponse;

            }
        }
    }

}

using ExternalUserApi.Model;
using ExternalUserApi.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ExternalUserApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        [Route("{accountId}/balance")]
        public async Task<decimal> GetUserAccountsAsync(string accountId)
        {
            try
            {
                return await _accountService.GetAccountBalanceAsync(accountId);
            }
            catch (Exception ex)
            {
                return -1;
            }
        }

        [HttpPost]
        [Route("{accountId}/debit")]
        public async Task<TransactionResponse> UpdateAccountBalanceAsync(string accountId, decimal debitBalance)
        {
            return
                await _accountService.DebitAccountBalanceAsync(accountId, debitBalance);
           
        }
    }


}

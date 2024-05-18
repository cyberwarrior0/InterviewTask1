using Newtonsoft.Json;
using System.Net.Http;
using System.Text.Json;
using WalletTopUp.Models;
using WalletTopUp.Repositories;

namespace WalletTopUp.Services
{
    public interface IWalletService
    {
        Task<string> TopUp(TopUpRequest topUpRequest);
    }

    public class WalletService : IWalletService
    {
        private readonly HttpClient _httpClient;
        private const int TransactionFee = 1;
        private readonly IUserRepository _userRepository;
        private readonly ITranscationRepository _transcationRepository;
        private readonly IBeneficiaryRepository _beneficiaryRepository;

        public WalletService(IUserRepository userRepository, ITranscationRepository transcationRepository, HttpClient httpClient, IBeneficiaryRepository beneficiaryRepository)
        {
            _userRepository = userRepository;
            _transcationRepository = transcationRepository;
            _httpClient = httpClient;
            _beneficiaryRepository= beneficiaryRepository;

        }
        public async Task<string> TopUp(TopUpRequest topUpRequest)
        {
            try
            {
                var userdetails = await _userRepository.GetUserById(topUpRequest.UserId);
                decimal maxTopUpLimit = userdetails.IsVerified ? 500 : 1000;
                if (userdetails.Beneficiaries.Count > 0 && !userdetails.Beneficiaries.Select(a => a.Id).Contains(topUpRequest.BeneficiaryId))
                {
                    return $"beneficiary does not exist for user {userdetails.Name}";
                }
                decimal totalTopUpAmount = await _transcationRepository.GetTotalTopUpAmountForMonth(topUpRequest.UserId);
                if (totalTopUpAmount + (int)topUpRequest.topUpOption > 3000)
                {
                    return $"Total monthly top-up limit for all beneficiaries exceeded for user {userdetails.Name}.";
                }
                decimal beneficiaryTopUpAmount = await _transcationRepository.GetTotalTransactionAmountForMonthByBeneficiary(topUpRequest.BeneficiaryId);
                if (beneficiaryTopUpAmount + (int)topUpRequest.topUpOption > maxTopUpLimit)
                {
                    return $"Monthly top-up limit for this beneficiary exceeded for user {userdetails.Name}.";
                }

                decimal userBalance = await GetUserBalanceAsync(userdetails.AccountId);
                if ((int)topUpRequest.topUpOption + TransactionFee > userBalance)
                {
                    return "Insufficient balance for top-up.";
                }
                var responseBody = await DebitUserBalanceAsync(userdetails.AccountId, (decimal)topUpRequest.topUpOption + TransactionFee);
                if (responseBody.status != "success")
                {
                    bool transacationFail = await _transcationRepository.AddTransacationRecord(topUpRequest, Status.Fail, responseBody.transactionId);
                    return "Failed to debit user balance.";
                }
                bool transacationResult = await _transcationRepository.AddTransacationRecord(topUpRequest, Status.Pass, responseBody.transactionId);
                bool topUpResult = await _beneficiaryRepository.TopUpBeneficaryAsync(topUpRequest);
                var beneficiary = await _beneficiaryRepository.GetBeneficiariesById(topUpRequest.BeneficiaryId);
                if (!topUpResult)
                {
                    return "Transcation failed";
                }
                int totalAmount = (int)topUpRequest.topUpOption + (int)TransactionFee;
                return "Wallet Top-up successful!! " + (int)topUpRequest.topUpOption + " AED added to the " + beneficiary.Nickname + " beneficary for user " + userdetails.Name + ". Transaction fee of 1 AED applied.";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
          }
        private async Task<decimal> GetUserBalanceAsync(string accountId)
        {
            try
            {
                var response = await _httpClient.GetAsync($"https://localhost:44327/api/Account/{accountId}/balance");
                response.EnsureSuccessStatusCode();

                var responseString = await response.Content.ReadAsStringAsync();
                var balance = System.Text.Json.JsonSerializer.Deserialize<decimal>(responseString);
                return balance;
            }
            catch(Exception ex)
            {
                return -1;
            }
            //return 5000;
        }
        private async Task<TransactionResponse> DebitUserBalanceAsync(string accountId, decimal amount)
        {
            try
            {
                var responseMessage = await _httpClient.PostAsJsonAsync($"https://localhost:44327/api/Account/{accountId}/debit", new { debitBalance = amount });
                responseMessage.EnsureSuccessStatusCode();
                var responseContent = await responseMessage.Content.ReadAsStringAsync();
                var transactionResponse = JsonConvert.DeserializeObject<TransactionResponse>(responseContent);
                return transactionResponse;
            }
            catch( Exception ex)
            {
                return new TransactionResponse
                {
                    status = "failed"
                };
            }
        }

    }
}

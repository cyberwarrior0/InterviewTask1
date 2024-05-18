using WalletTopUp.Models;

namespace WalletTopUp.Repositories
{
    public interface ITranscationRepository
    {
        Task<decimal> GetTotalTransactionAmountForMonthByBeneficiary(int beneficaryId);
        Task<decimal> GetTotalTopUpAmountForMonth(int userId);
        Task<bool> AddTransacationRecord(TopUpRequest topUpRequest,Status status,string transactionId);
    }
}

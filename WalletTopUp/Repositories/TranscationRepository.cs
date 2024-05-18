using Microsoft.EntityFrameworkCore;
using WalletTopUp.Data;
using WalletTopUp.Models;

namespace WalletTopUp.Repositories
{
    public class TranscationRepository : ITranscationRepository
    {
        private readonly AppDbContext _context;

        public TranscationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetTotalTransactionAmountForMonthByBeneficiary(int BeneficiaryId)
        {
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1);

            var totalAmount =await _context.Transactions
                .Where(t => t.BeneficiaryId == BeneficiaryId && t.TimeStamp >= startDate && t.TimeStamp < endDate && t.TransactionType==TransactionType.Credit)
                .SumAsync(t => t.Amount);

            return totalAmount;
        }
        public async Task<decimal> GetTotalTopUpAmountForMonth(int UserId)
        {
            var now = DateTime.Now;
            var startDate = new DateTime(now.Year, now.Month, 1);
            var endDate = startDate.AddMonths(1);

            var totalAmount = await _context.Transactions
                .Where(t => t.UserId == UserId && t.TimeStamp >= startDate && t.TimeStamp < endDate && t.TransactionType == TransactionType.Credit)
                .SumAsync(t => t.Amount);

            return totalAmount;
        }
        public async Task<bool> AddTransacationRecord(TopUpRequest topUpRequest, Status status, string transactionId)
        {
            try
            {
                var transactionRecord = new Transaction
                {
                    UserId = topUpRequest.UserId,
                    BeneficiaryId = topUpRequest.BeneficiaryId,
                    Amount = (decimal)topUpRequest.topUpOption,
                    TimeStamp = DateTime.Now,
                    Status= status,
                    BankTranscationId= transactionId,
                    TransactionType=TransactionType.Credit
                };
                await _context.Transactions.AddAsync(transactionRecord);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

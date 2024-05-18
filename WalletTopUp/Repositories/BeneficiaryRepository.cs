using Microsoft.EntityFrameworkCore;
using WalletTopUp.Data;
using WalletTopUp.Models;

namespace WalletTopUp.Repositories
{
    public class BeneficiaryRepository : IBeneficiaryRepository
    {
        private readonly AppDbContext _context;

        public BeneficiaryRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<bool> TopUpBeneficaryAsync(TopUpRequest topUpRequest)
        {
            var beneficiary = await _context.Beneficiaries.SingleOrDefaultAsync(b => b.UserId == topUpRequest.UserId && b.Id == topUpRequest.BeneficiaryId);

            if (beneficiary == null)
            {
                return false;
            }

            beneficiary.Balance += (decimal)topUpRequest.topUpOption;
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Beneficiary>> GetBeneficiariesByUserId(int userId)
        {
            return await _context.Beneficiaries
                .Where(b => b.UserId == userId && b.IsActive)
                .ToListAsync();
        }
        public async Task<Beneficiary> GetBeneficiariesById(int beneficiaryId)
        {
            return await _context.Beneficiaries
                .Where(b => b.Id == beneficiaryId)
                .FirstOrDefaultAsync();
        }


    }
}

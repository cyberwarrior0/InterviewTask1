using Microsoft.EntityFrameworkCore;
using WalletTopUp.Data;
using WalletTopUp.Models;

namespace WalletTopUp.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;
        private readonly IBeneficiaryRepository _beneficiaryRepository; 

        public UserRepository(AppDbContext context, IBeneficiaryRepository beneficiaryRepository)
        {
            _context = context;
            _beneficiaryRepository= beneficiaryRepository;
        }
        public async Task<UserDetails> GetUserById(int userId)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null)
            {
                return null;
            }

            var userDetails = new UserDetails
            {
                Id = user.Id,
                Name = user.Name,
                MobileNumber = user.MobileNumber,
                IsVerified = user.IsVerified,
                Beneficiaries = await _beneficiaryRepository.GetBeneficiariesByUserId(userId),
                AccountId=user.AccountId
            };

            return userDetails;
        }
    }
}

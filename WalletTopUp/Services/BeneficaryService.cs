using WalletTopUp.Models;
using WalletTopUp.Repositories;

namespace WalletTopUp.Services
{
    public interface IBeneficiaryService
    {
        Task<List<Beneficiary>> GetBeneficiariesByUserId(int userId);
        Task<Beneficiary> GetBeneficiariesById(int beneficaryId);

        decimal GetBalance(int id);
    }
    public class BeneficiaryService : IBeneficiaryService
    {
        private readonly IBeneficiaryRepository _beneficiaryRepository;

        public BeneficiaryService(IBeneficiaryRepository beneficiaryRepository)
        {
            _beneficiaryRepository = beneficiaryRepository;
        }

        public async Task<List<Beneficiary>> GetBeneficiariesByUserId(int userId)
        {
            return await _beneficiaryRepository.GetBeneficiariesByUserId(userId);
        }
        public async Task<Beneficiary> GetBeneficiariesById(int beneficiaryId)
        {
            return await _beneficiaryRepository.GetBeneficiariesById(beneficiaryId);
        }
        public decimal GetBalance(int id)
        {
                return 0;
        }

    }

}

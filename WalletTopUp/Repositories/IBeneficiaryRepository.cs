using WalletTopUp.Models;

namespace WalletTopUp.Repositories
{
    public interface IBeneficiaryRepository
    {
        Task<List<Beneficiary>> GetBeneficiariesByUserId(int userId);
        Task<Beneficiary> GetBeneficiariesById(int id);
        Task<bool>  TopUpBeneficaryAsync(TopUpRequest topUpRequest);

    }
}

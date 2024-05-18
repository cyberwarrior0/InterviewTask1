namespace WalletTopUp.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string MobileNumber { get; set; }
        public bool IsVerified { get; set; }
        public string AccountId { get; set; }

    }
    public class UserDetails: User
    {
        public List<Beneficiary> Beneficiaries { get; set; }
    }
}

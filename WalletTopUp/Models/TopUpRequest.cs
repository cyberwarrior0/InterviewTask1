namespace WalletTopUp.Models
{
    public class TopUpRequest
    {
        public TopUpOption topUpOption { get; set; }
        public int BeneficiaryId { get; set; }
        public int UserId { get; set; }
    }

}

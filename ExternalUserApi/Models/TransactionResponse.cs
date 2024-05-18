namespace ExternalUserApi.Model
{
    public class TransactionResponse
    {
        public string transactionId { get; set; }
        public decimal updatedBalance { get; set; }
        public DateTime timeStamp { get; set; }
        public String status { get; set; }

    }
}

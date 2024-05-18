namespace WalletTopUp.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime TimeStamp { get; set; }
        public int BeneficiaryId { get; set; }
        public int UserId { get; set; }
        public TransactionType TransactionType { get; set; } 
        public Status Status { get; set; }
        public string BankTranscationId { get; set; }

    }

    public enum TransactionType
    {
        Debit,
        Credit
    }
    public enum Status
    {
        Fail,
        Pass
    }

    public class TransactionResponse
    {
        public string transactionId { get; set; }
        public decimal updatedBalance { get; set; }
        public DateTime timeStamp { get; set; }
        public String status { get; set; }

    }

}
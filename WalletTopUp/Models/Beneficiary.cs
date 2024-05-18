using System.ComponentModel.DataAnnotations;

namespace WalletTopUp.Models
{
    public class Beneficiary
    {
        public int Id { get; set; }

        [StringLength(20, ErrorMessage = "Nickname must be at most 20 characters long.")]
        public string Nickname { get; set; }
        public bool IsActive { get; set; }
        public decimal Balance { get; set; }
        public int UserId { get; set; }

    }

}

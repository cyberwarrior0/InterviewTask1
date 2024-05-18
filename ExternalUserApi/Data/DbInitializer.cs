
using ExternalUserApi.Model;

namespace ExternalUserApi.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Accounts.Any())
            {
                return;   
            }

            var accounts = new Account[]
            {
                new Account { AccountHolderName = "Alice", MobileNumber = "1234567890", BranchCode = "HDFC001",AccountId="5000155890",Balance=7800895},
                new Account { AccountHolderName = "Bob", MobileNumber = "0987654321", BranchCode = "BOA7855",AccountId="79851055890",Balance=9520006 }
            };

            context.Accounts.AddRange(accounts);
            context.SaveChanges();

        }
    }
}

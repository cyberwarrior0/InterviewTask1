using WalletTopUp.Models;

namespace WalletTopUp.Data
{
    public class DbInitializer
    {
        public static void Initialize(AppDbContext context)
        {
            if (context.Users.Any())
            {
                return;   
            }

            var users = new User[]
            {
                new User { Name = "Alice", MobileNumber = "1234567890", IsVerified = true,AccountId="5000155890"},
                new User { Name = "Bob", MobileNumber = "0987654321", IsVerified = false,AccountId="79851055890" }
            };

            context.Users.AddRange(users);
            context.SaveChanges();
            var beneficiaries = new Beneficiary[]
              {
                    new Beneficiary { Nickname = "AliceB1", IsActive = true, Balance = 1000, UserId = users[0].Id },
                    new Beneficiary { Nickname = "BobB1", IsActive = false, Balance = 2300,UserId = users[0].Id  },
                    new Beneficiary { Nickname = "BobB2", IsActive = false, Balance = 5000,UserId = users[1].Id},
                    new Beneficiary { Nickname = "BobB3", IsActive = true, Balance = 10000, UserId = users[0].Id }

              };

            context.Beneficiaries.AddRange(beneficiaries);
            context.SaveChanges();

        }
    }
}

using BankingApplication.Models;

namespace BankingApplication.Data;

public static class SeedData
{
    public static void InitDb(IServiceProvider serviceProvider)
    {
        var context = serviceProvider.GetRequiredService<BankingApplicationContext>();

        // check if DB is already seeded
        if (context.Customers.Any())
            return;

        var customers = GetCustomers();

        // insert customers in DB 
        foreach (var customer in customers)
        {
            context.Customers.Add(
                new Customer
                {
                    CustomerID = customer.CustomerID,
                    Name = customer.Name,
                    Address = customer.Address,
                    City = customer.City,
                    Postcode = customer.Postcode,
                    Islocked = false
                }
            );
            // insert accounts in DB
            foreach (var account in customer.Accounts)
            {
                context.Accounts.Add(
                    new Account
                    {
                        AccountNumber = account.AccountNumber,
                        AccountType = account.AccountType,
                        CustomerID = account.CustomerID,
                        Balance = getInitialBalance(account)
                    }
                );
                // insert transactions into DB
                foreach (var transaction in account.Transactions)
                {

                    context.Transactions.Add(
                        new Transaction
                        {   
                            TransactionType = "D",
                            AccountNumber = account.AccountNumber,
                            Amount = transaction.Amount,
                            Comment = transaction.Comment,
                            TransactionTimeUtc = transaction.TransactionTimeUtc
                        }
                    );
                }
            }
            // insert login into DB
            context.Logins.Add(
                new Login
                {
                    LoginID = customer.Login.LoginID,
                    CustomerID = customer.CustomerID,
                    PasswordHash = customer.Login.PasswordHash
                }
            );

            // commit to DB 
            context.SaveChanges();
        }
    
    }

    public static decimal getInitialBalance(Account account)
    {
        decimal balance = 0;

        foreach(var transaction in account.Transactions)
        {
            balance += transaction.Amount;
        }

        return balance;
    }

    public static List<Customer> GetCustomers()
    {

        const string format = "dd/MM/yyyy hh:mm:ss tt";

        return new List<Customer>
        {
            new Customer
            {
                CustomerID = 2100,
                Name = "Derek Tek",
                Address = "123 Fake Street",
                City = "Melbourne",
                Postcode = "3000",
                Islocked = false,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountNumber = 4100,
                        AccountType = "S",
                        CustomerID = 2100,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Amount = 100.00M,
                                Comment = "Opening balance",
                                TransactionTimeUtc = DateTime.ParseExact("02/01/2024 08:00:00 PM", format, null)
                            }
                        }
                    },
                    new Account
                    {
                        AccountNumber = 4101,
                        AccountType = "C",
                        CustomerID = 2100,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Amount = 600.00M,
                                Comment = "First deposit",
                                TransactionTimeUtc = DateTime.ParseExact("02/01/2024 08:30:00 PM", format, null)
                            },

                            new Transaction
                            {
                                Amount = 300.00M,
                                Comment = "Second deposit",
                                TransactionTimeUtc = DateTime.ParseExact("02/01/2024 08:45:00 PM", format, null)
                            }
                            
                        }

                    }
                },
                Login = new Login
                {
                    LoginID = "12345678",
                    CustomerID = 2100,
                    PasswordHash = "Rfc2898DeriveBytes$50000$MrW2CQoJvjPMlynGLkGFrg==$x8iV0TiDbEXndl0Fg8V3Rw91j5f5nztWK1zu7eQa0EE="
                }
            },
            new Customer
            {
                CustomerID = 2200,
                Name = "Guest",
                Address = "23 Random Street",
                City = "Melbourne",
                Postcode = "3000",
                Islocked = false,
                Accounts = new List<Account>
                {
                    new Account
                    {
                        AccountNumber = 4200,
                        AccountType = "S",
                        CustomerID = 2200,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Amount = 100.00M,
                                Comment = "Opening balance",
                                TransactionTimeUtc = DateTime.ParseExact("01/02/2024 08:00:00 PM", format, null)
                            }
                        }
                    },
                    new Account
                    {
                        AccountNumber = 4201,
                        AccountType = "C",
                        CustomerID = 2200,
                        Transactions = new List<Transaction>
                        {
                            new Transaction
                            {
                                Amount = 600.00M,
                                Comment = "First deposit",
                                TransactionTimeUtc = DateTime.ParseExact("01/02/2024 08:30:00 PM", format, null)
                            },

                            new Transaction
                            {
                                Amount = 300.00M,
                                Comment = "Second deposit",
                                TransactionTimeUtc = DateTime.ParseExact("01/02/2024 08:45:00 PM", format, null)
                            }
                            
                        }

                    }
                },
                Login = new Login
                {
                    LoginID = "01234567",
                    CustomerID = 2200,
                    PasswordHash = "Rfc2898DeriveBytes$50000$MrW2CQoJvjPMlynGLkGFrg==$x8iV0TiDbEXndl0Fg8V3Rw91j5f5nztWK1zu7eQa0EE="
                }
            }
        };
    }
}
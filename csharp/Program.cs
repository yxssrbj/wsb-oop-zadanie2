var bank = new Bank("WSB Bank");
bank.OpenAccount("PL001", "Anna Kowalska", 1000);
bank.OpenAccount("PL002", "Tomasz Nowak");

bank.Get("PL001").Deposit(500);
bank.Get("PL001").Withdraw(200);
bank.Transfer("PL001", "PL002", 300);
try
{
    bank.Get("PL002").Withdraw(5000);
}
catch (InvalidOperationException e)
{
    Console.WriteLine("Error: " + e.Message);
}

bank.Get("PL001").PrintHistory();
bank.Get("PL002").PrintHistory();
bank.PrintSummary();

class Transaction
{
    public string Kind { get; }
    public decimal Amount { get; }
    public string Description { get; }
    public DateTime Date { get; } = DateTime.Now;

    public Transaction(string kind, decimal amount, string description)
    {
        Kind = kind;
        Amount = amount;
        Description = description;
    }

    public override string ToString() => $"{Date:yyyy-MM-dd HH:mm}  {Kind,-12} {Amount,10:F2}  {Description}";
}

class Account
{
    public string Number { get; }
    public string Owner { get; }
    public decimal Balance { get; private set; }
    private readonly List<Transaction> history = new();
    public IReadOnlyList<Transaction> History => history;

    public Account(string number, string owner, decimal balance = 0)
    {
        Number = number;
        Owner = owner;
        Balance = balance;
    }

    public void Deposit(decimal amount, string description = "Deposit")
    {
        if (amount <= 0) throw new InvalidOperationException("Amount must be positive.");
        Balance += amount;
        history.Add(new Transaction("DEPOSIT", amount, description));
    }

    public void Withdraw(decimal amount, string description = "Withdrawal")
    {
        if (amount <= 0) throw new InvalidOperationException("Amount must be positive.");
        if (amount > Balance) throw new InvalidOperationException("Insufficient funds.");
        Balance -= amount;
        history.Add(new Transaction("WITHDRAWAL", amount, description));
    }

    public void PrintHistory()
    {
        Console.WriteLine($"History of {Number} ({Owner}):");
        foreach (var t in history) Console.WriteLine("   " + t);
        Console.WriteLine($"   Balance: {Balance:F2}");
    }
}

class Bank
{
    public string Name { get; }
    private readonly Dictionary<string, Account> accounts = new();

    public Bank(string name) => Name = name;

    public Account OpenAccount(string number, string owner, decimal balance = 0)
    {
        if (accounts.ContainsKey(number)) throw new InvalidOperationException($"Account {number} already exists.");
        var account = new Account(number, owner, balance);
        accounts[number] = account;
        return account;
    }

    public Account Get(string number) =>
        accounts.TryGetValue(number, out var a) ? a : throw new InvalidOperationException($"Account {number} not found.");

    public void Transfer(string fromNumber, string toNumber, decimal amount)
    {
        Account source = Get(fromNumber), target = Get(toNumber);
        source.Withdraw(amount, $"Transfer to {toNumber}");
        target.Deposit(amount, $"Transfer from {fromNumber}");
    }

    public void PrintSummary()
    {
        Console.WriteLine($"Bank {Name}:");
        foreach (var a in accounts.Values) Console.WriteLine($"   {a.Number}  {a.Owner,-15} {a.Balance,10:F2}");
    }
}

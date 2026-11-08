from datetime import datetime


class Transaction:
    def __init__(self, kind, amount, description=""):
        self.kind = kind
        self.amount = amount
        self.description = description
        self.date = datetime.now()

    def __str__(self):
        return f"{self.date:%Y-%m-%d %H:%M}  {self.kind:<12} {self.amount:>10.2f}  {self.description}"


class Account:
    def __init__(self, number, owner, balance=0.0):
        self.number = number
        self.owner = owner
        self._balance = balance
        self.history = []

    @property
    def balance(self):
        return self._balance

    def deposit(self, amount, description="Deposit"):
        if amount <= 0:
            raise ValueError("Amount must be positive.")
        self._balance += amount
        self.history.append(Transaction("DEPOSIT", amount, description))

    def withdraw(self, amount, description="Withdrawal"):
        if amount <= 0:
            raise ValueError("Amount must be positive.")
        if amount > self._balance:
            raise ValueError("Insufficient funds.")
        self._balance -= amount
        self.history.append(Transaction("WITHDRAWAL", amount, description))

    def print_history(self):
        print(f"History of {self.number} ({self.owner}):")
        for t in self.history:
            print("  ", t)
        print(f"   Balance: {self._balance:.2f}")


class Bank:
    def __init__(self, name):
        self.name = name
        self.accounts = {}

    def open_account(self, number, owner, balance=0.0):
        if number in self.accounts:
            raise ValueError(f"Account {number} already exists.")
        account = Account(number, owner, balance)
        self.accounts[number] = account
        return account

    def get(self, number):
        if number not in self.accounts:
            raise ValueError(f"Account {number} not found.")
        return self.accounts[number]

    def transfer(self, from_number, to_number, amount):
        source, target = self.get(from_number), self.get(to_number)
        source.withdraw(amount, f"Transfer to {to_number}")
        target.deposit(amount, f"Transfer from {from_number}")

    def print_summary(self):
        print(f"Bank {self.name}:")
        for a in self.accounts.values():
            print(f"   {a.number}  {a.owner:<15} {a.balance:>10.2f}")


if __name__ == "__main__":
    bank = Bank("WSB Bank")
    bank.open_account("PL001", "Anna Kowalska", 1000)
    bank.open_account("PL002", "Tomasz Nowak")

    bank.get("PL001").deposit(500)
    bank.get("PL001").withdraw(200)
    bank.transfer("PL001", "PL002", 300)
    try:
        bank.get("PL002").withdraw(5000)
    except ValueError as e:
        print("Error:", e)

    bank.get("PL001").print_history()
    bank.get("PL002").print_history()
    bank.print_summary()

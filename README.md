# OOP – Task 2: Classes and objects

**Topic 5 – Banking system:** classes `Account`, `Transaction`, `Bank`; deposits, withdrawals, transfers and transaction history. Implemented in **Python** and **C#**.

## Classes

| Class | Responsibility | Properties | Methods |
|-------|----------------|------------|---------|
| `Transaction` | One operation on an account | kind, amount, description, date | `__str__` / `ToString` |
| `Account` | Holds balance and history; validates operations | number, owner, balance (read-only), history | `deposit`, `withdraw`, `print_history` |
| `Bank` | Opens accounts, finds them by number, transfers money | name, accounts | `open_account`, `get`, `transfer`, `print_summary` |

Relations: a `Bank` has many `Account`s (dictionary by account number), an `Account` has many `Transaction`s (list). The balance can only change through `deposit`/`withdraw`, which reject non-positive amounts and overdrafts.

## Run

```bash
cd python && python3 bank.py
cd csharp && dotnet run      # .NET 8
```

Online: GitHub Codespaces (`.devcontainer` included) or [vscode.dev](https://vscode.dev/github/yxssrbj/wsb-oop-zadanie2).

## Example output

```
Error: Insufficient funds.
History of PL001 (Anna Kowalska):
   2026-11-08 23:28  DEPOSIT          500.00  Deposit
   2026-11-08 23:28  WITHDRAWAL       200.00  Withdrawal
   2026-11-08 23:28  WITHDRAWAL       300.00  Transfer to PL002
   Balance: 1000.00
History of PL002 (Tomasz Nowak):
   2026-11-08 23:28  DEPOSIT          300.00  Transfer from PL001
   Balance: 300.00
Bank WSB Bank:
   PL001  Anna Kowalska      1000.00
   PL002  Tomasz Nowak        300.00
```


## Use of AI

AI (Claude, https://claude.ai, 08.11.2026) was used to help write the code and README; I reviewed and tested it.

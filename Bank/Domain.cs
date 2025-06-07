namespace Bank;

record Transfer(Account From, Account To, Transaction Transaction)
{
    public static TransferFrom FromAccount(Account from) => new(from);

    public record TransferFrom(Account From)
    {
        public TransferTransactionBuilder TransferTo(Account to) => new(From, to);
    }

    public record TransferTransactionBuilder(Account From, Account To)
    {
        public Transfer Amount(Transaction transaction) => new(From, To, transaction);
    }

    public static IEnumerable<Account> Execute(Transfer transfer)
    {
        yield return transfer.From.Withdraw(transfer.Transaction);
        yield return transfer.To.Deposit(transfer.Transaction);
    }
}

record Account(IEnumerable<Statement> Statements)
{
    public static Account Empty => new Account([]);

    public Account Withdraw(Transaction transaction) =>
        new Account(Statements: Statements.Append(Statement.StartingFrom(Balance).Withdraw(transaction)));

    public Account Deposit(Transaction transaction) =>
        new Account(Statements.Append(Statement.StartingFrom(Balance).Deposit(transaction)));

    public Money Balance => Option.Of(Statements.LastOrDefault())
        .Map(StatementBalance)
        .Reduce(Money.Zero);

    private static Money StatementBalance(Statement x) => x.BalanceAfterTransaction;
}


enum Type
{
    Withdraw,
    Deposit
}

record Statement(Type Type, Transaction Transaction, Money BalanceAfterTransaction)
{
    public static StatementBuilder StartingFrom(Money startingBalance) => new StatementBuilder(startingBalance);

    public record StatementBuilder(Money StartingBalance)
    {
        public Statement Withdraw(Transaction transaction) =>
            new Statement(Type.Withdraw, transaction, transaction.SubstractFrom(StartingBalance));

        public Statement Deposit(Transaction transaction) =>
            new Statement(Type.Deposit, transaction, transaction.AddTo(StartingBalance));
    }
}

record Transaction(Money Amount, DateTime Date)
{
    public record TransactionBuilder(int Amount)
    {
        public Transaction On(DateTime date) => new(Amount, date);
    }

    public static TransactionBuilder Of(int amount) => new(amount);

    public Money SubstractFrom(Money balance) => balance - Amount;

    public Money AddTo(Money balance) => balance + Amount;
}

record Money(int Value)
{
    public static Money Zero => new(0);
    public static implicit operator Money(int value) => new(value);
    public static Money operator +(Money a, Money b) => new(a.Value + b.Value);
    public static Money operator -(Money a, Money b) => new(a.Value - b.Value);
}
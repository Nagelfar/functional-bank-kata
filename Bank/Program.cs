using Bank;

Console.WriteLine("1. use case: Withdrawal and Deposits");
Console.Out.WriteLine(
    StatementPrinter.DefaultPrinter.AsString(
        PrintableStatement.All.Withdrawals.From(
            Account.Empty
                .Deposit(Transaction.Of(200).On(DateTime.Parse("2024-10-10")))
                .Withdraw(Transaction.Of(100).On(DateTime.Parse("2024-11-10")))
                .Withdraw(Transaction.Of(100).On(DateTime.Parse("2024-12-10")))
        )
    )
);

Console.WriteLine("2. use case: Transfer between accounts");

var first = Account.Empty
    .Deposit(Transaction.Of(200).On(DateTime.Parse("2024-10-10")));
var second =
    Account.Empty;

Transfer.Execute(
        Transfer
            .FromAccount(first)
            .TransferTo(second)
            .Amount(
                Transaction.Of(100).On(DateTime.Parse("2024-11-10"))
            )
    )
    .Select(PrintableStatement.All.From)
    .Select(StatementPrinter.DefaultPrinter.AsString)
    .ToList()
    .ForEach(Console.Out.WriteLine);
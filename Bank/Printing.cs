namespace Bank;

record StatementPrinter(string LineSeparator, string ColumnSeparator)
{
    public static StatementPrinter DefaultPrinter => new(Environment.NewLine, "\t");

    private string Line(IEnumerable<string> elements) => string.Join(ColumnSeparator, elements);

    public string AsString(PrintableStatement printable) =>
        string.Join(
            LineSeparator,
            printable.StatementLines
                .Select(Line)
                .Prepend(Line(printable.HeaderElements))
                .Append(string.Empty)
        );
}

record PrintableStatement(IEnumerable<IEnumerable<string>> StatementLines)
{
    public static PrintableStatementFilter All => new(PrintableStatementFilter.AllStatements);

    public record PrintableStatementFilter(Func<Statement, bool> Filter)
    {
        public PrintableStatementFilter Withdrawals => new(Filter: WithdrawlsOnly);
        public PrintableStatementFilter Deposits => new(Filter: DepositsOnly);
        public PrintableStatementFilter OnDate(DateTime date) => new(Filter: OnDateOnly(date));

        internal static bool AllStatements(Statement statement) => true;
        private static bool WithdrawlsOnly(Statement statement) => statement.Type == Type.Withdraw;
        private static bool DepositsOnly(Statement statement) => statement.Type == Type.Deposit;
        private static Func<Statement, bool> OnDateOnly(DateTime date) => new DateFilter(date).Filter;

        private record DateFilter(DateTime Date)
        {
            public bool Filter(Statement statement) => statement.Transaction.Date == Date;
        }

        public PrintableStatement From(Account account) =>
            PrintableStatement.PreparePrint(account.Statements.Where(Filter));
    }

    private static string PrintMoney(Money money) => $"{money.Value}";
    private static string PrintDate(DateTime date) => date.ToShortDateString();

    private static IEnumerable<string> LineElements(Statement statement)
    {
        switch (statement)
        {
            case Statement(Type.Withdraw, var transaction, var balance):
                yield return PrintDate(transaction.Date);
                yield return "-" + PrintMoney(transaction.Amount);
                yield return PrintMoney(balance);
                yield break;
            case Statement (Type.Deposit, var transaction, var balance):
                yield return PrintDate(transaction.Date);
                yield return "+" + PrintMoney(transaction.Amount);
                yield return PrintMoney(balance);
                yield break;
            default:
                throw new ArgumentOutOfRangeException(nameof(statement));
        }
    }

    private static PrintableStatement PreparePrint(IEnumerable<Statement> statements) =>
        new PrintableStatement(statements.Select(LineElements));

    public IEnumerable<string> HeaderElements
    {
        get
        {
            yield return "Date".PadRight("DD.MM.YYYY".Length, ' ');
            yield return "Amount";
            yield return "Balance";
        }
    }
}
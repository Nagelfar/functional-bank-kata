

// Console.WriteLine(Fibonacci.recursive(15));
//
// Console.WriteLine(Fibonacci.nonRecursive(15));
//
// Console.WriteLine(Fibonacci.nonRecursiveIterator(15));
// Console.WriteLine(Fibonacci.nonRecursiveInfinite(15));

public static class Fibonacci
{
    public static long recursive(int n) => n <= 1 ? 1 : recursive(n - 1) + recursive(n - 2);

    public static long nonRecursive(int n)
    {
        var a = 1;
        var b = 1;
        int c = 0;
        for (var i = 2; i <= n; i++)
        {
            c = a + b;
            a = b;
            b = c;
        }

        return c;
    }

    public static long nonRecursiveIterator(int n)
    {
        return Enumerable.Range(2, n - 1)
            .Aggregate(new { a = 1, b = 1 }, (acc, i) => new { a = acc.b, b = acc.a + acc.b }, acc => acc.b);
    }

    private static IEnumerable<long> InfiniteSequence(int start)
    {
        while (true)
            yield return start++;
    }

    public static long nonRecursiveInfinite(int n)
    {
        return InfiniteSequence(2)
            .Take(n - 1)
            .Aggregate(new { a = 1, b = 1 }, (acc, i) => new { a = acc.b, b = acc.a + acc.b }, acc => acc.b);
    }
}
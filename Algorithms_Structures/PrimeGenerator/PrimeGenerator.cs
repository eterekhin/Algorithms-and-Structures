namespace Algorithms_Structures.PrimeGenerator;

public class PrimeGenerator(int count)
{
    private class PrimeNumberMultiply(long number, long multiply)
    {
        public long Multiply { get; set; } = multiply;
        public long Number { get; } = number;
    }

    private readonly List<PrimeNumberMultiply> _primeMultiplies = new(count);

    public IEnumerable<long> Generate()
    {
        yield return 2;
        _primeMultiplies.Add(new PrimeNumberMultiply(2, 2 * 2));

        long value = 2;
        var obtained = 1;
        while (value++ < long.MaxValue)
        {
            if (obtained == count) yield break;
            
            if (!IsPrime(value)) continue;
            obtained++;
            _primeMultiplies.Add(new PrimeNumberMultiply(value, value * 2));
            yield return value;
        }
    }

    private bool IsPrime(long value)
    {
        foreach (var item in _primeMultiplies)
        {
            while (item.Multiply < value)
                item.Multiply += item.Number;

            if (item.Multiply == value)
                return false;
        }

        return true;
    }
}
namespace Algorithms_Structures.PrimeGenerator;

public class PrimeGenerator(int count)
{
    private class PrimeNumberMultiply(long number)
    {
        public long Multiply { get; set; } = 2 * number;
        public long Number { get; } = number;
    }

    private readonly List<PrimeNumberMultiply> _primeMultiplies = new(count);

    public IEnumerable<long> Generate()
    {
        yield return 2;
        _primeMultiplies.Add(new PrimeNumberMultiply(2));

        long value = 1;
        var obtained = 1;
        while (value != long.MaxValue)
        {
            if (obtained == count) yield break;
            
            value += 2;
            if (!IsPrime(value)) continue;
            obtained++;
            _primeMultiplies.Add(new PrimeNumberMultiply(value));
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
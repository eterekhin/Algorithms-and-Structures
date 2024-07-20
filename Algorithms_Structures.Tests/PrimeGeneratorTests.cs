namespace Algorithms_Structures.Tests;

public class PrimeGeneratorTests
{
    [Fact]
    public void ReferenceTest()
    {
        const int count = 10;
        var actual = new PrimeGenerator.PrimeGenerator(count).Generate();
        var expected = GeneratePrimesNaively(count);
        Assert.Equal(expected, actual);
    }

    private IEnumerable<long> GeneratePrimesNaively(int count)
    {
        List<long> result = new(count) { 2 };
        long value = 2;
        while (value++ != long.MaxValue)
        {
            if (result.Count == count)
                return result;

            for (var i = 2; i < value; i++)
            {
                if (value % i == 0)
                    break;
                
                if (i + 1 == value)
                    result.Add(value);
            }
        }
        
        return result;
    }
}
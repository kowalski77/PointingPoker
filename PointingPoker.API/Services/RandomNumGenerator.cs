using System.Security.Cryptography;

namespace PointingPoker.API.Services;

public sealed class RandomNumGenerator :  IRandomNumGenerator, IDisposable
{
    private readonly RandomNumberGenerator randomNumberGenerator;

    public RandomNumGenerator()
    {
        this.randomNumberGenerator = RandomNumberGenerator.Create();
    }

    public int GetRandomNumber(int min, int max)
    {
        var randomNumber = new byte[1];
        this.randomNumberGenerator.GetBytes(randomNumber);
        var asciiValueOfRandomCharacter = Convert.ToDouble(randomNumber[0]);
        var multiplier = Math.Max(0, (asciiValueOfRandomCharacter / 255d) - 0.00000000001d);
        double range = max - min + 1;
        var randomValueInRange = Math.Floor(multiplier * range);
        
        return (int)(min + randomValueInRange);
    }

    public void Dispose()
    {
        this.randomNumberGenerator.Dispose();
    }
}

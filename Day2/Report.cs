namespace Day2;

public class Report
{
    public int[] Numbers { get; }
    
    public Report(string line)
    {
        var items = line.Split(" ").Select(i => int.Parse(i.Trim())).ToArray();
        Numbers = items;
    }

    public bool IsSafe()
    {
        // Determine direction
        if (Numbers.Length < 2)
        {
            return false;
        }
        
        var isAscendingReport = Numbers[1] > Numbers[0];
        Console.WriteLine("---");
        foreach (var couple in Numbers.Zip(Numbers.Skip(1)))
        {
            Console.Write($"Couple {couple.First} {couple.Second} --> ");
            if (couple.First == couple.Second)
            {
                // Equal numbers ==> Unsafe
                Console.WriteLine("equal -> unsafe");
                return false;
            }

            // Ascending report and decreasing numbers => Unsafe
            if (isAscendingReport && couple.First > couple.Second)
            {
                Console.WriteLine("first > second and ascending -> unsafe");
                return false;
            }

            if (!isAscendingReport && couple.First < couple.Second)
            {
                Console.WriteLine("first < second and not ascending -> unsafe");
                return false;
            }

            var levelDifference = couple.First > couple.Second ? couple.First - couple.Second : couple.Second - couple.First;
            if (levelDifference > 3)
            {
                Console.WriteLine("level diff > 3 -> unsafe");
                return false;
            }

            Console.WriteLine("OK");
        }

        return true;
    }
}


namespace Day2;

public static class Program
{
    public static void Main(string[] args)
    {
        var content = File.ReadAllText("files/input.txt").Trim().Split("\n");
        var reports = content.Select(line => new Report(line)).ToArray();

        var safeReportsCount = reports.Count(r => r.IsSafe());
        Console.WriteLine($"Part 1: Safe reports count {safeReportsCount}");
    }
}


using System.Text.RegularExpressions;

namespace Day3;

public partial class Program
{
    public static void Main()
    {
        var content = File.ReadAllText("files/input.txt").Trim();
        Part1(content);
    }

    private static void Part1(string content)
    {
        var mulInstructions = MulInstructionFinder().Matches(content);
        var result = mulInstructions
            .Select(match =>
            {
                Console.WriteLine($"Found {match.Value}");
                return (int.Parse(match.Groups["t1"].Value), int.Parse(match.Groups["t2"].Value));
            })
            .Sum(item => item.Item1 * item.Item2);
        
        Console.WriteLine("Part 1: " + result);
    }

    [GeneratedRegex(@"mul\((?<t1>\d+),(?<t2>\d+)\)")]
    private static partial Regex MulInstructionFinder();
}
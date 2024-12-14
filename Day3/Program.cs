
using System.Text.RegularExpressions;

namespace Day3;

public class Program
{
    public static void Main()
    {
        var content = File.ReadAllText("files/input.txt").Replace("\n", "").Trim();
        Part1(content);
        Part2(content);
    }

    private static void Part1(string content)
    {
        var mulInstructions = MulInstructionFinder.Matches(content);
        var result = mulInstructions
            .Select(match =>
            {
                Console.WriteLine($"Found: {match.Value}");
                return (int.Parse(match.Groups["t1"].Value), int.Parse(match.Groups["t2"].Value));
            })
            .Sum(item => item.Item1 * item.Item2);
        
        Console.WriteLine("Part 1: " + result);
    }

    private static void Part2(string content)
    {
        var instructions = ConditionalMulInstructionFinder.Matches(content);
        var result = instructions.Aggregate(new { Acc = 0, MulEnabled = true }, (state, instruction) =>
        {
            Console.WriteLine($"Found {instruction.Value}");
            
            if (instruction.Value.Equals("do()"))
            {
                return state with { MulEnabled = true };
            }

            if (instruction.Value.Equals("don't()"))
            {
                return state with { MulEnabled = false };
            }

            if (!instruction.Value.StartsWith("mul(")) throw new ArgumentOutOfRangeException(nameof(instruction), instruction.Value);
            
            if (!state.MulEnabled)
            {
                return state;
            }
                
            var (t1, t2) = (int.Parse(instruction.Groups["t1"].Value), int.Parse(instruction.Groups["t2"].Value));
            return state with { Acc = state.Acc + t1 * t2 };
        });

        Console.WriteLine("Part 2: " + result.Acc);
    }

    private static Regex MulInstructionFinder = new(@"mul\((?<t1>\d+),(?<t2>\d+)\)");
    private static Regex ConditionalMulInstructionFinder = new(@"do(?:n't)?\(\)|mul\((?<t1>\d+),(?<t2>\d+)\)");
}

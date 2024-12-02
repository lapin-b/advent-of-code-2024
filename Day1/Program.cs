
namespace Day1;

public static class Program
{
    public static void Main(string[] args)
    {
        var content = File.ReadAllText("files/input.txt").Trim().Split("\n").ToArray();
        var leftList = new List<int>();
        var rightList = new List<int>();

        foreach (var line in content)
        {
            var parts = line.Split("   ");
            leftList.Add(int.Parse(parts[0]));
            rightList.Add(int.Parse(parts[1]));
        }

        // Part 1
        Part1(leftList, rightList);
        Part2(leftList, rightList);
    }

    private static void Part1(List<int> leftList, List<int> rightList)
    {
        var clonedLeftList = new List<int>(leftList);
        var clonedRightList = new List<int>(rightList);
        var differences = new List<int>();

        for (var i = 0; i < leftList.Count; i++)
        {
            var minLeft = clonedLeftList.Min();
            var minRight = clonedRightList.Min();
            var difference = minLeft > minRight ? minLeft - minRight : minRight - minLeft;
            
            clonedLeftList.Remove(minLeft);
            clonedRightList.Remove(minRight);

            Console.WriteLine($"{minLeft} => {minRight} --> {difference}");
            differences.Add(difference);
        }
        
        var total = differences.Sum();
        Console.WriteLine($"Part 1: Total differences: {total}");
    }

    private static void Part2(List<int> leftList, List<int> rightList)
    {
        var similarities = new List<int>();
        
        foreach (var itemLeft in leftList)
        {
            var rightListItemFrequency = rightList.Count(x => x == itemLeft);
            var similarityScore = itemLeft * rightListItemFrequency;
            similarities.Add(similarityScore);

            Console.WriteLine($"{itemLeft} appears {rightListItemFrequency} times --> {similarityScore}");
        }
        
        Console.WriteLine($"Part 2: Total similarities: {similarities.Sum()}");
    }
}
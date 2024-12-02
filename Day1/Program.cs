
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
}
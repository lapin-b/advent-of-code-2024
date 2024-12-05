namespace Day5;

class Program
{
    static void Main(string[] args)
    {
        var groups = File
            .ReadAllText("files/input.txt")
            .Trim()
            .Split(Environment.NewLine + Environment.NewLine);

        var sortRules = groups[0].Split(Environment.NewLine)
            .Select(line =>
            {
                var ruleParts = line.Split('|');
                return new PageSortRule(
                    int.Parse(ruleParts[1]),
                    int.Parse(ruleParts[0])
                );
            })
            .ToArray();

        var updates = groups[1]
            .Split(Environment.NewLine)
            .Select(line => line.Split(",").Select(int.Parse).ToArray())
            .ToArray();

        // Part 1
        var correctlyPrintedUpdatesMiddlePageTotal = updates
            .Where(u => IsCorrectlySorted(u, sortRules))
            .Select(u => u[u.Length / 2])
            .Sum();
        
        Console.WriteLine($"Part 1: {correctlyPrintedUpdatesMiddlePageTotal}");
    }

    private static bool IsCorrectlySorted(int[] pages, PageSortRule[] rules)
    {
        var applicableRules = rules
            .Where(r => pages.Contains(r.OrderingBoundary) && pages.Contains(r.PrintedBeforeBoundary))
            .ToArray();

        foreach (var rule in applicableRules)
        {
            var orderingBoundaryPosition = pages
                .Select((page, index) => new { Page = page, Index = index })
                .First(p => p.Page == rule.OrderingBoundary)
                .Index;
            
            var printedBeforeBoundaryPosition = pages.
                Select((page, index) => new { Page = page, Index = index })
                .First(p => p.Page == rule.PrintedBeforeBoundary)
                .Index;

            if (printedBeforeBoundaryPosition > orderingBoundaryPosition)
            {
                return false;
            }
        }

        return true;
    }
}

internal record PageSortRule(int OrderingBoundary, int PrintedBeforeBoundary);
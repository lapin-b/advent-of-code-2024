using AOCUtils;

namespace Day6;

public class GuardPatrolingMap: Grid<char>
{
    public HashSet<GridPoint> VisitedPoints { get; } = [];
    
    public GuardPatrolingMap(string[] lines): base(lines.Select(l => l.ToCharArray()).ToArray())
    {
        
    }

    public void MarkAsVisited(GridPoint point)
    {
        VisitedPoints.Add(point);
    }
}
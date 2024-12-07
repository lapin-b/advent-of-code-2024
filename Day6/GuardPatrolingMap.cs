using AOCUtils;

namespace Day6;

public class GuardPatrolingMap: Grid<char>
{
    public GuardPatrolingMap(string[] lines): base(lines.Select(l => l.ToCharArray()).ToArray())
    {
        
    }
}
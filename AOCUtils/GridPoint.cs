namespace AOCUtils;

public readonly struct GridPoint(int x, int y)
{
    public int X { get; } = x;
    public int Y { get; } = y;

    public GridPoint AddVector(GridRelativeVector direction, int distance)
    {
        if (distance == 0)
        {
            return this;
        }
        
        return direction switch
        {
            GridRelativeVector.Top => new(X, Y - distance),
            GridRelativeVector.Down => new(X, Y + distance),
            GridRelativeVector.Left => new(X - distance, Y),
            GridRelativeVector.Right => new(X + distance, Y),
            GridRelativeVector.TopRight => new(X + distance, Y - distance),
            GridRelativeVector.DownRight => new(X + distance, Y + distance),
            GridRelativeVector.TopLeft => new(X - distance, Y - distance),
            GridRelativeVector.DownLeft => new(X - distance, Y + distance),
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };        
    }
    
    public override string ToString() => $"({X},{Y})";
}
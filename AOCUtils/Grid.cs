namespace AOCUtils;

public class Grid<TGridItem> where TGridItem: struct
{
    private TGridItem[][] _grid;
    public bool CoalesceNonExistingValues { get; set; } = true;

    public Grid(TGridItem[][] grid)
    {
        _grid = grid;
    }

    public TGridItem? this[int x, int y]
    {
        get
        {
            if (!CoordinatesAreInGridBounds(x, y))
            {
                return CoalesceNonExistingValues ? null : throw new IndexOutOfRangeException($"({x}, {y}) is not a valid coordinate in the grid");
            }

            return _grid[y][x];
        }
    }

    public TGridItem? this[GridPoint point] => this[point.X, point.Y];

    public TGridItem? ElementAtRelative(int x, int y, GridRelativeVector direction, int relativePosition)
    {
        if (relativePosition == 0)
        {
            return this[x, y];
        }
        
        return direction switch
        {
            GridRelativeVector.Top => this[x, y - relativePosition],
            GridRelativeVector.Down => this[x, y + relativePosition],
            GridRelativeVector.Left => this[x - relativePosition, y],
            GridRelativeVector.Right => this[x + relativePosition, y],
            GridRelativeVector.TopRight => this[x + relativePosition, y - relativePosition],
            GridRelativeVector.DownRight => this[x + relativePosition, y + relativePosition],
            GridRelativeVector.TopLeft => this[x - relativePosition, y - relativePosition],
            GridRelativeVector.DownLeft => this[x - relativePosition, y + relativePosition],
            _ => throw new ArgumentOutOfRangeException(nameof(direction), direction, null)
        };
    }
    
    public TGridItem? ElementAtRelative(GridPoint point, GridRelativeVector direction, int relativePosition) =>
        ElementAtRelative(point.X, point.Y, direction, relativePosition);
    
    public IEnumerable<LetterGridItem<TGridItem>> Traverse()
    {
        for (var y = 0; y < _grid.Length; y++)
        {
            for (var x = 0; x < _grid[y].Length; x++)
            {
                yield return new LetterGridItem<TGridItem>(new GridPoint(x, y), _grid[y][x]);
            }
        }
    }

    protected bool CoordinatesAreInGridBounds(int x, int y) => 
        y >= 0 && y < _grid.Length && 
        x >= 0 && x < _grid[y].Length;
}

public record LetterGridItem<T>(GridPoint Point, T Item);
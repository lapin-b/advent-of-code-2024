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

    public TGridItem? this[GridPoint point]
    {
        get
        {
            if (!CoordinatesAreInGridBounds(point.X, point.Y))
            {
                return CoalesceNonExistingValues ? null : throw new IndexOutOfRangeException($"({point}) is not a valid coordinate in the grid");
            }

            return _grid[point.Y][point.X];
        }
    }

    [Obsolete("Use GridPoint.AddVector and this[gridPoint] instead")]
    public TGridItem? ElementAtRelative(int x, int y, GridRelativeVector direction, int relativePosition) => 
        ElementAtRelative(new GridPoint(x, y), direction, relativePosition);

    [Obsolete("Use GridPoint.AddVector and this[gridPoint] instead")]
    public TGridItem? ElementAtRelative(GridPoint point, GridRelativeVector direction, int relativePosition) => 
        this[point.AddVector(direction, relativePosition)];

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
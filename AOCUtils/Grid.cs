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
            if (
                y < 0 || y > _grid.Length - 1 ||
                x < 0 || x > _grid[y].Length - 1
            )
            {
                return CoalesceNonExistingValues ? null : throw new IndexOutOfRangeException($"({x}, {y}) is not a valid coordinate in the grid");
            }

            return _grid[y][x];
        }
    }

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
    
    public IEnumerable<LetterGridItem<TGridItem>> Traverse()
    {
        for (var y = 0; y < _grid.Length; y++)
        {
            for (var x = 0; x < _grid[y].Length; x++)
            {
                yield return new LetterGridItem<TGridItem>(x, y, _grid[y][x]);
            }
        }
    }
}

public record LetterGridItem<T>(int X, int Y, T Item);
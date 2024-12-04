namespace Day4;

public class LetterGrid
{
    private char[][] _grid;

    public LetterGrid(string[] grid)
    {
        _grid = grid.Select(line => line.ToCharArray()).ToArray();
    }

    public char? ElementAt(int x, int y)
    {
        if (
            y < 0 ||
            y > _grid.Length - 1 ||
            x < 0 ||
            x > _grid[y].Length - 1
        )
        {
            return null;
        }
        
        return _grid[y][x];
    }

    public IEnumerable<LetterGridItem> GridElements()
    {
        for (var y = 0; y < _grid.Length; y++)
        {
            for (var x = 0; x < _grid[y].Length; x++)
            {
                yield return new LetterGridItem(x, y, _grid[y][x]);
            }
        }
    }

    public int CountXmasAt(int x, int y)
    {
        var count = 0;

        // Left to right
        if (this[x, y] == 'X' && this[x + 1, y] == 'M' && this[x + 2, y] == 'A' && this[x + 3, y] == 'S')
        {
            count++;
        }
        
        // Right to left
        if (this[x, y] == 'S' && this[x + 1, y] == 'A' && this[x + 2, y] == 'M' && this[x + 3, y] == 'X')
        {
            count++;
        }
        
        // Top to bottom
        if (this[x, y] == 'X' && this[x, y + 1] == 'M' && this[x, y + 2] == 'A' && this[x, y + 3] == 'S')
        {
            count++;
        }
        
        // Bottom to top
        if (this[x, y] == 'S' && this[x, y + 1] == 'A' && this[x, y + 2] == 'M' && this[x, y + 3] == 'X')
        {
            count++;
        }
        
        // Top-left to bottom-right diagonal
        if (this[x, y] == 'X' && this[x + 1, y + 1] == 'M' && this[x + 2, y + 2] == 'A' && this[x + 3, y + 3] == 'S')
        {
            count++;
        }
        
        // Bottom-right to top-left diagonal
        if (this[x, y] == 'S' && this[x + 1, y + 1] == 'A' && this[x + 2, y + 2] == 'M' && this[x + 3, y + 3] == 'X')
        {
            count++;
        }
        
        // Top-right to bottom-left diagonal 
        if (this[x, y] == 'X' && this[x - 1, y + 1] == 'M' && this[x - 2 , y + 2] == 'A' && this[x - 3, y + 3] == 'S')
        {
            count++;
        }
        
        // Bottom-left to top-right diagonal 
        if (this[x, y] == 'S' && this[x - 1, y + 1] == 'A' && this[x - 2 , y + 2] == 'M' && this[x - 3, y + 3] == 'X')
        {
            count++;
        }
        
        return count;
    }

    public char? this[int x, int y] => ElementAt(x, y);
}

public record LetterGridItem(int X, int Y, char Letter);
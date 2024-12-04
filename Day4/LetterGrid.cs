using AOCUtils;

namespace Day4;

public class LetterGrid : Grid<char>
{
    // Part 1
    public LetterGrid(string[] grid) : base(grid.Select(line => line.ToCharArray()).ToArray())
    {
    }

    public int CountXmasAt(int x, int y)
    {
        var count = 0;

        // Left to right
        if (
            this[x, y] == 'X'
            && ElementAtRelative(x, y, GridRelativeVector.Right, 1) == 'M'
            && ElementAtRelative(x, y, GridRelativeVector.Right, 2) == 'A'
            && ElementAtRelative(x, y, GridRelativeVector.Right, 3) == 'S'
        )
        {
            count++;
        }

        // Right to left
        if (
            this[x, y] == 'S'
            && ElementAtRelative(x, y, GridRelativeVector.Right, 1) == 'A'
            && ElementAtRelative(x, y, GridRelativeVector.Right, 2) == 'M'
            && ElementAtRelative(x, y, GridRelativeVector.Right, 3) == 'X'
        )
        {
            count++;
        }

        // Top to bottom
        if (
            this[x, y] == 'X' &&
            ElementAtRelative(x, y, GridRelativeVector.Top, 1) == 'M' &&
            ElementAtRelative(x, y, GridRelativeVector.Top, 2) == 'A' &&
            ElementAtRelative(x, y, GridRelativeVector.Top, 3) == 'S'
        )
        {
            count++;
        }

        // Bottom to top
        if (
            this[x, y] == 'S' &&
            ElementAtRelative(x, y, GridRelativeVector.Top, 1) == 'A' &&
            ElementAtRelative(x, y, GridRelativeVector.Top, 2) == 'M' &&
            ElementAtRelative(x, y, GridRelativeVector.Top, 3) == 'X'
        )
        {
            count++;
        }

        // Top-left to bottom-right diagonal
        if (
            this[x, y] == 'X' &&
            ElementAtRelative(x, y, GridRelativeVector.DownRight, 1) == 'M' &&
            ElementAtRelative(x, y, GridRelativeVector.DownRight, 2) == 'A' &&
            ElementAtRelative(x, y, GridRelativeVector.DownRight, 3) == 'S'
        )
        {
            count++;
        }

        // Bottom-right to top-left diagonal
        if (
            this[x, y] == 'S' &&
            ElementAtRelative(x, y, GridRelativeVector.DownRight, 1) == 'A' &&
            ElementAtRelative(x, y, GridRelativeVector.DownRight, 2) == 'M' &&
            ElementAtRelative(x, y, GridRelativeVector.DownRight, 3) == 'X'
        )
        {
            count++;
        }

        // Top-right to bottom-left diagonal 
        if (
            this[x, y] == 'X' &&
            ElementAtRelative(x, y, GridRelativeVector.DownLeft, 1) == 'M' &&
            ElementAtRelative(x, y, GridRelativeVector.DownLeft, 2) == 'A' &&
            ElementAtRelative(x, y, GridRelativeVector.DownLeft, 3) == 'S'
        )
        {
            count++;
        }

        // Bottom-left to top-right diagonal 
        if (
            this[x, y] == 'S' &&
            ElementAtRelative(x, y, GridRelativeVector.DownLeft, 1) == 'A' &&
            ElementAtRelative(x, y, GridRelativeVector.DownLeft, 2) == 'M' &&
            ElementAtRelative(x, y, GridRelativeVector.DownLeft, 3) == 'X'
        )
        {
            count++;
        }

        return count;
    }

    // Part 2
    public int CountMasInXAt(int x, int y)
    {
        if (this[x, y] != 'A')
        {
            return 0;
        }

        var count = 0;
        // Top-left to bottom-right diagonal || bottom-right to top-left diagonal
        if (
            (
                ElementAtRelative(x, y, GridRelativeVector.TopLeft, 1) == 'M' &&
                ElementAtRelative(x, y, GridRelativeVector.DownRight, 1) == 'S'
            ) || (
                ElementAtRelative(x, y, GridRelativeVector.TopLeft, 1) == 'S' &&
                ElementAtRelative(x, y, GridRelativeVector.DownRight, 1) == 'M'
            )
        )
        {
            count++;
        }

        // Top-right to bottom-left diagonal || bottom-left to top-right diagonal 
        if (
            (
                ElementAtRelative(x, y, GridRelativeVector.TopRight, 1) == 'M' &&
                ElementAtRelative(x, y, GridRelativeVector.DownLeft, 1) == 'S'
            ) || (
                ElementAtRelative(x, y, GridRelativeVector.TopRight, 1) == 'S' &&
                ElementAtRelative(x, y, GridRelativeVector.DownLeft, 1) == 'M'
            )
        )
        {
            count++;
        }

        return count == 2 ? 1 : 0;
    }
}

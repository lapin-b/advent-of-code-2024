namespace Day4;

class Program
{
    static void Main(string[] args)
    {
        var content = File.ReadAllText("files/input.txt").Trim().Split(Environment.NewLine);
        var grid = new LetterGrid(content);

        var xmasCount = grid.Traverse().Sum(element => grid.CountXmasAt(element.Point.X, element.Point.Y));
        var masCount = grid.Traverse().Sum(element => grid.CountMasInXAt(element.Point.X, element.Point.Y));

        Console.WriteLine($"Part 1: XMAS count: {xmasCount}");
        Console.WriteLine($"Part 2: X-MAS count: {masCount}");
    }
}
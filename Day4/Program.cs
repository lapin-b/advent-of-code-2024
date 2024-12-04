namespace Day4;

class Program
{
    static void Main(string[] args)
    {
        var content = File.ReadAllText("files/input.txt").Trim().Split(Environment.NewLine);
        var grid = new LetterGrid(content);

        var acc = 0;
        foreach (var element in grid.GridElements())
        {
            //Console.WriteLine($"{element.X}, {element.Y} = {element.Letter}");
            acc += grid.CountXmasAt(element.X, element.Y);
        }

        Console.WriteLine("Acc: " + acc);
    }
}
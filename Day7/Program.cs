namespace Day7;

class Program
{
    static void Main(string[] args)
    {
        var fileContent = File.ReadAllLines("files/input.txt");
        var equations = fileContent.Select(l => new CalibrationLine(l)).ToArray();

        var chronoStart = DateTime.Now;
        foreach (var equation in equations)
        {
            Console.WriteLine(equation);
            equation.SolveEquation();
        }

        var chronoFinish = DateTime.Now;

        Console.WriteLine($"Solved {equations.Length} in {(chronoFinish - chronoStart).TotalSeconds} seconds");
        
        // Part 1
        var solvedEquationsTotal = equations
            .Where(e => e.IsSolved == SolvedState.Solved)
            .Aggregate((ulong)0, (acc, current) => acc + current.ExecuteCalculation());

        Console.WriteLine($"Part 1: Sum of solved equations: {solvedEquationsTotal}");
    }
}
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
            Console.Write(equation);
            equation.SolveEquation();

            if (equation.IsSolved == SolvedState.Solved)
            {
                Console.WriteLine(" --> " + string.Join(", ", equation.Operators));
            }
            else
            {
                Console.WriteLine(" --> Impossible");
            }
        }

        var chronoFinish = DateTime.Now;

        Console.WriteLine($"Solved {equations.Length} equations in {(chronoFinish - chronoStart).TotalSeconds} seconds");
        
        // Part 1
        var solvedEquationsTotal = equations
            .Where(e => e.IsSolved == SolvedState.Solved && !e.Operators.Contains(Operator.Concat))
            .Aggregate((ulong)0, (acc, current) => acc + current.ExecuteCalculation());

        Console.WriteLine($"Part 1: Sum of solved equations: {solvedEquationsTotal}");
        
        // Part 2
        // var part2SolvedEquationsTotal = equations
        //     .Where(e => e.IsSolved == SolvedState.Solved)
        //     .Aggregate((ulong)0, (acc, current) => acc + current.ExecuteCalculation());
        //
        // Console.WriteLine($"Part 2: Sum of solved equations: {part2SolvedEquationsTotal}");
    }
}
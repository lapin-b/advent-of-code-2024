using System.Diagnostics;
using System.Runtime.InteropServices.ComTypes;
using System.Text.RegularExpressions;

namespace Day7;

public class CalibrationLine
{
    private ulong _target;
    private ulong[] _operands;
    private Operator[] _operators = [];
    private string _originalString;
    public SolvedState IsSolved { get; private set; } = SolvedState.NotTried;
    public Operator[] Operators => _operators;

    private static Regex _extractionRegex = new(@"(?<Result>\d+): (?:(?<Operand>\d+) ?)+");
    
    public CalibrationLine(string line)
    {
        var components = _extractionRegex.Match(line.Trim());
        _operands = components.Groups["Operand"].Captures.Select(c => ulong.Parse(c.Value)).ToArray();
        _target = ulong.Parse(components.Groups["Result"].Value);
        _originalString = line;
    }

    public void SolveEquation()
    {
        SolveEquationRecursive([], _operands[0], 1);
        
        // Perform sanity checks
        if (IsSolved == SolvedState.Solved)
        {
            if (_operators.Length >= _operands.Length)
            {
                throw new Exception($"Operators count > operands count: {_operators.Length} >= {_operands.Length}");
            }

            var actualCalculation = ExecuteCalculation();
            if (actualCalculation != _target)
            {
                throw new Exception($"Equality is not upheld (target: {_target}, actual: {actualCalculation})");
            }
        }

        // Still marked as not tried ? Must be impossible to solve then.
        if (IsSolved == SolvedState.NotTried)
        {
            IsSolved = SolvedState.ImpossibleToSolve;
        }
    }

    public ulong ExecuteCalculation()
    {
        return _operands[1..]
            .Zip(_operators)
            .Aggregate(_operands[0], (acc, current) => ExecuteOperator(current.Second, acc, current.First));
    }

    public override string ToString() => _originalString;

    private void SolveEquationRecursive(List<Operator> operators, ulong accumulator, int currentOperandPosition)
    {
        // End conditions
        // 1. Accumulator is equal to the target
        if (accumulator == _target)
        {
            _operators = operators.ToArray();
            IsSolved = SolvedState.Solved;
            return;
        }
        
        // 2. Ran out of operands
        // 3. Operators array is already filled
        if (currentOperandPosition > _operands.Length - 1 || _operators.Length > 0)
        {
            return;
        }

        var currentOperand = _operands[currentOperandPosition];
        
        // Step
        
        // Note for part 2: Concatenating throws everything off because of implmentation details
        // The concatenation operator is commented for this reason
        foreach (var op in new[] { Operator.Add, Operator.Mul, /*Operator.Concat*/})
        {
            operators.Add(op);
            SolveEquationRecursive(
                operators, 
                ExecuteOperator(op, accumulator, currentOperand),
                currentOperandPosition + 1
            );
            operators.RemoveAt(operators.Count - 1);

            if (IsSolved == SolvedState.Solved)
            {
                break;
            }
        }
    }

    private ulong ExecuteOperator(Operator op, ulong op1, ulong op2) => op switch
    {
        Operator.Mul => op1 * op2,
        Operator.Add => op1 + op2,
        Operator.Concat => ulong.Parse(op1.ToString() + op2.ToString()),
        _ => throw new ArgumentOutOfRangeException(nameof(op), op, null)
    };
}

public enum Operator
{
    Mul,
    Add,
    Concat,
}

public enum SolvedState
{
    Solved,
    NotTried,
    ImpossibleToSolve,
}
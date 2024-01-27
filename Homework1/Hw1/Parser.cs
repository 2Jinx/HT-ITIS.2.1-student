namespace Hw1;

public static class Parser
{
    public static void ParseCalcArguments(string[] args, 
        out double val1, 
        out CalculatorOperation operation, 
        out double val2)
    {
        val1 = val2 = 0;
        operation = CalculatorOperation.Undefined;

        if (!IsArgLengthSupported(args))
            throw new ArgumentException("Incorrect input");

        if (double.TryParse(args[0], out var value1) && double.TryParse(args[2], out var value2))
        {
            val1 = value1;
            val2 = value2;
            operation = ParseOperation(args[1]);
        }
        else
        {
            throw new ArgumentException("Incorrect input"); 
        }
    }

    private static bool IsArgLengthSupported(string[] args) => args.Length == 3;

    private static CalculatorOperation ParseOperation(string arg)
    {
        return arg switch
        {
            "+" => CalculatorOperation.Plus,
            "-" => CalculatorOperation.Minus,
            "*" => CalculatorOperation.Multiply,
            "/" => CalculatorOperation.Divide,
            _ => throw new InvalidOperationException("Invalid operation!")
        };
    }
}
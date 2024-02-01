using System.Globalization;

namespace Hw8.Calculator;

public class Parser
{
    public static CalculatorArgs ParseCalculatorArgs(string value1, string operation, string value2)
    {
        CalculatorArgs args = new CalculatorArgs();
        args.Value1 = ParseStringToDouble(value1);
        args.Operation = ParseOperation(operation);
        args.Value2 = ParseStringToDouble(value2);

        return args;
    }
    
    private static Operation ParseOperation(string operation)
    {
        return operation.ToLower() switch
        {
            "plus" => Operation.Plus, 
            "minus" => Operation.Minus, 
            "multiply" => Operation.Multiply,
            "divide" => Operation.Divide,
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
    }

    private static double ParseStringToDouble(string val)
    {
        if (!Double.TryParse(val, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
            throw new ArgumentException(Messages.InvalidNumberMessage);

        return value;
    }
}
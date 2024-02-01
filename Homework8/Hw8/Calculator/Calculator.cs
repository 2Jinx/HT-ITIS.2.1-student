namespace Hw8.Calculator;

public class Calculator: ICalculator
{
    public double Calculate(CalculatorArgs args)
    {
        return args.Operation switch
        {
            Operation.Plus => Plus(args.Value1, args.Value2),
            Operation.Minus => Minus(args.Value1, args.Value2),
            Operation.Multiply => Multiply(args.Value1, args.Value2),
            Operation.Divide => Divide(args.Value1, args.Value2),
            _ => throw new InvalidOperationException(Messages.InvalidOperationMessage)
        };
    }
    public double Plus(double val1, double val2) => val1 + val2;
    
    public double Minus(double val1, double val2) => val1 - val2;
    
    public double Multiply(double val1, double val2) => val1 * val2;

    public double Divide(double firstValue, double secondValue)
    {
        if (secondValue == 0)
            throw new ArgumentException(Messages.DivisionByZeroMessage);

        return firstValue / secondValue;
    }
}
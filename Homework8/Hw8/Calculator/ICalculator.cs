namespace Hw8.Calculator;

public interface ICalculator
{
    public double Calculate(CalculatorArgs args);
    public double Plus(double val1, double val2);
    
    public double Minus(double val1, double val2);
    
    public double Multiply(double val1, double val2);
    
    public double Divide(double firstValue, double secondValue);
}
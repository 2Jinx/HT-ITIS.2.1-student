namespace Hw1;
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("Expression: " + args[0] + args[1] + args[2]);
            Parser.ParseCalcArguments(args, out var val1, out var operation, out var val2);
            Console.WriteLine("Result: " + Calculator.Calculate(val1, operation, val2)); 
        }
        catch(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.Message);
            Console.ResetColor();
        }
    }
}
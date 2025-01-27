using Hw2;
using Tests.RunLogic.Attributes;

namespace Tests.CSharp.Homework2;

public class CalculatorTests
{
    [HomeworkTheory(Homeworks.HomeWork2)]
    [InlineData(15, 5, CalculatorOperation.Plus, 20)]
    [InlineData(15, 5, CalculatorOperation.Minus, 10)]
    [InlineData(15, 5, CalculatorOperation.Multiply, 75)]
    [InlineData(15, 5, CalculatorOperation.Divide, 3)]
    public void TestAllOperations(int value1, int value2, CalculatorOperation operation, int expectedValue)
    {
        var actualValue = Calculator.Calculate(value1, operation, value2);
        
        Assert.Equal(expectedValue, actualValue);
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestInvalidOperation()
    {
        Assert.Throws<InvalidOperationException>(() => Calculator.Calculate(120, CalculatorOperation.Undefined, 10));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingNonZeroByZero()
    {
        Assert.Equal(Double.PositiveInfinity,  Calculator.Calculate(12, CalculatorOperation.Divide, 0));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByNonZero()
    {
        Assert.Equal(0,Calculator.Calculate(0, CalculatorOperation.Divide, 10));
    }

    [Homework(Homeworks.HomeWork2)]
    public void TestDividingZeroByZero()
    {
        Assert.Equal(Double.NaN, Hw1.Calculator.Calculate(0, Hw1.CalculatorOperation.Divide, 0));
    }
}
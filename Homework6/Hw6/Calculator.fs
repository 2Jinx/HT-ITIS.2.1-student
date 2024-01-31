module Hw6.Calculator

type CalculatorOperation =
     | Plus = 0
     | Minus = 1
     | Multiply = 2
     | Divide = 3


[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline calculate (value1: double, operation , value2: double) =
     match operation with
     | Error message -> Error message
     | Ok oper ->
          match oper with
          | CalculatorOperation.Plus -> Ok $"{value1 + value2}"
          | CalculatorOperation.Minus -> Ok $"{value1 - value2}"
          | CalculatorOperation.Multiply -> Ok $"{value1 * value2}"
          | CalculatorOperation.Divide ->
               if value2 = 0.0 then
                    Error "Error! Dividing by zero!"
               else
                    Ok $"{value1 / value2}"

    
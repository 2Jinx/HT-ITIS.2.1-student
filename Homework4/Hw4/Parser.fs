module Hw4.Parser

open System
open Hw4.Calculator


type CalcOptions = {
    arg1: float
    arg2: float
    operation: CalculatorOperation
}

let isArgLengthSupported (args : string[]) =
    Array.length args = 3

let parseOperation (arg : string) =
    match arg with
    | "+" -> CalculatorOperation.Plus
    | "-" -> CalculatorOperation.Minus
    | "*" -> CalculatorOperation.Multiply
    | "/" -> CalculatorOperation.Divide
    | _ -> raise (System.ArgumentException("Incorrect Input!"))
    
let parseCalcArguments (args : string[]) =
    if not (isArgLengthSupported args) then
        raise (System.ArgumentException("Incorrect Input!"))

    let mutable arg1 = 0.0
    let mutable arg2 = 0.0

    match Double.TryParse(args.[0], &arg1), Double.TryParse(args.[2], &arg2) with
    | true, true ->
        let operation = parseOperation args.[1]
        { arg1 = arg1; operation = operation; arg2 = arg2 }
    | _ ->
        raise (System.ArgumentException("Incorrect Input!"))



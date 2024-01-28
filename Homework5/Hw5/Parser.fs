module Hw5.Parser

open System
open Hw5.Calculator
open Hw5.MaybeBuilder

let isArgLengthSupported (args:string[]): Result<string[], Message> =
    if args.Length = 3 then
        Ok args
    else
        Error Message.WrongArgLength
    
[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isOperationSupported (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    match operation with
    | Plus -> Ok (arg1, CalculatorOperation.Plus, arg2)
    | Minus -> Ok (arg1, CalculatorOperation.Minus, arg2)
    | Multiply -> Ok (arg1, CalculatorOperation.Multiply, arg2)
    | Divide -> Ok (arg1, CalculatorOperation.Divide, arg2)
    | _ -> Error Message.WrongArgFormatOperation

let parseArgs (args: string[]): Result<(float * CalculatorOperation * float), Message> =
    match isArgLengthSupported args with
    | Error Message.WrongArgLength -> Error Message.WrongArgLength  
    | Ok args ->
        let mutable arg1 = 0.0
        let mutable arg2 = 0.0

        match Double.TryParse(args[0], &arg1), Double.TryParse(args[2], &arg2) with
        | true, true -> isOperationSupported (arg1, args[1], arg2)
        | _ -> Error Message.WrongArgFormat  

[<System.Diagnostics.CodeAnalysis.ExcludeFromCodeCoverage>]
let inline isDividingByZero (arg1, operation, arg2): Result<('a * CalculatorOperation * 'b), Message> =
    if (operation = CalculatorOperation.Divide && arg2 = 0.0) then
        Error Message.DivideByZero
    else
        Ok (arg1, operation, arg2)
    
let parseCalcArguments (args: string[]): Result<'a, 'b> =
     maybe{
         let! argsSupported = args |> isArgLengthSupported 
         let! parsedArgs = argsSupported |> parseArgs
         let! checkedArgs = parsedArgs |> isDividingByZero
         
         return checkedArgs
     }


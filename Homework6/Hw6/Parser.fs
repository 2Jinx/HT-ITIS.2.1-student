module Hw6.Parser

open System
open System.Net
open Hw6.Calculator
open System.Globalization

let parseOperation (operation: string) =
    let oper = operation.Split('=').[1]
    match oper with
    | "Plus" -> Ok CalculatorOperation.Plus
    | "Minus" -> Ok CalculatorOperation.Minus
    | "Multiply" -> Ok CalculatorOperation.Multiply
    | "Divide" -> Ok CalculatorOperation.Divide
    | _ -> Error $"Could not parse value '{WebUtility.UrlDecode(oper)}'"

let parseStringToDouble (arg : string) =
    let argument = arg.Split('=').[1]
    match Double.TryParse(argument, NumberStyles.Float, CultureInfo.InvariantCulture) with
    | (true, value) -> Ok value
    | _ -> Error $"Could not parse value '{argument}'"
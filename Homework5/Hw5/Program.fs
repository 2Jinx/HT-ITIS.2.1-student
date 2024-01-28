open Hw5
open Hw5.Parser
open Hw5.Calculator

let Main args =
    try
        match parseCalcArguments args with
        | Ok (arg1, CalculatorOperation, arg2) ->
            let result = calculate arg1 CalculatorOperation arg2
            printf "Result: %f" result
        | Error message ->
            match message with
            | Message.WrongArgLength -> printf "Parameter count mismatch!"
            | Message.WrongArgFormat -> printf "Incorrect input!"
            | Message.WrongArgFormatOperation -> printf "Invalid operation!"
            | Message.DivideByZero -> printf "Divide by zero!"
    with
        | ex -> printf "Exception: %s" ex.Message 
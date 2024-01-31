module Hw6.App

open Microsoft.AspNetCore.Hosting
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.Hosting
open Microsoft.Extensions.Logging
open Microsoft.Extensions.DependencyInjection
open Giraffe

let doCalculations (urlArgs: string[]) =
    let value1 = Parser.parseStringToDouble urlArgs.[0]
    let operation = Parser.parseOperation urlArgs.[1]
    let value2 = Parser.parseStringToDouble urlArgs.[2]
    match value1 with
    | Ok val1 ->
        match value2 with
        | Ok val2 -> Calculator.calculate (val1, operation, val2)
        | Error message -> Error message
    | Error message -> Error message
    
let calculatorHandler: HttpHandler =
    fun next ctx ->
        let queryString = ctx.Request.QueryString.Value
        let urlArgs = queryString.Trim('?').Split('&')
        
        let result: Result<string, string> = doCalculations urlArgs
        
        match result with
        | Ok ok -> (setStatusCode 200 >=> text (ok.ToString())) next ctx
        | Error error -> (setStatusCode 400 >=> text error) next ctx

let webApp =
    choose [
        GET >=> choose [
             route "/" >=> text "Use //calculate?value1=<VAL1>&operation=<OPERATION>&value2=<VAL2>"
             route "/calculate" >=> calculatorHandler
        ]
        setStatusCode 404 >=> text "Not Found" 
    ]
    
type Startup() =
    member _.ConfigureServices (services : IServiceCollection) =
        services.AddGiraffe() |> ignore

    member _.Configure (app : IApplicationBuilder) (_ : IHostEnvironment) (_ : ILoggerFactory) =
        app.UseGiraffe webApp
        
[<EntryPoint>]
let main _ =
    Host
        .CreateDefaultBuilder()
        .ConfigureWebHostDefaults(fun whBuilder -> whBuilder.UseStartup<Startup>() |> ignore)
        .Build()
        .Run()
    0
// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Library

[<EntryPoint>]
let main argv =
    printfn "The simple calculator"
    let environment = Map.ofList [ "a", 1  ]
    // Create an expression tree that represents
    // the expression: a + 2.
    let expr1 = Expr(Add(ValueOf "a", Number 2))
    // Evaluate the expression #1, given the
    // table of values for the variables.
    let (env, exp) = evaluate (environment, expr1)
    printfn "Result #1: %d" (getResult exp)

    let se = Add(ValueOf("a"), Number(2))
    let expr2 = Set("b", se)
    let (env, exp) = evaluate (environment, expr2)
    printfn "Result #2: %d" (getResult exp)

    let expr3 = Seq([expr1; expr2; Set("c", Multiply(ValueOf("b"), Number(2)))])
    let (env, exp) = evaluate (environment, expr3)
    printfn "Result #3: %d" (getResult exp)

    // Test the errors
    try
        let expr4 = Seq([expr1; expr2; Set("c", Multiply(ValueOf("x"), Number(2)))])
        let (env, exp) = evaluate (environment, expr4)
        printfn "Result @E#1: %d" (getResult exp)
    with
    | ex -> printfn "Error: %s" (ex.ToString())

    try
        printfn "Result @E#2.a: %d" (getResult expr1)
    with
    | ex -> printfn "Error: %s" (ex.ToString())

    try
        printfn "Result @E#2.b: %d" (getResult expr2)
    with
    | ex -> printfn "Error: %s" (ex.ToString())
        
    0 // return an integer exit code


// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open Library

[<EntryPoint>]
let main argv =
    printfn "The simple calculator"
    let environment = Map.ofList [ "a", 1 ; "b", 2 ; "c", 3 ]
    // Create an expression tree that represents
    // the expression: a + 2 * b.
    let expr1 = Add(GetVar "a", Multiply(Number 2, GetVar "b"))
    // Evaluate the expression #1, given the
    // table of values for the variables.
    let result = Evaluate environment expr1
    printfn "Result #1: %d" result

    let expr2 = Sub(GetVar "a", Divide(Number 10, GetVar "b"))
    let result = Evaluate environment expr2
    printfn "Result #2: %d" result

    let expr3 = Neg(Sub(GetVar "a", Divide(Number 10, If(Number 1, GetVar "b", Number 0))))
    let result = Evaluate environment expr3
    printfn "Result #3: %d" result

    let expr4 = SetVar("c" , Neg(Sub(GetVar "a", Divide(Number 10, If(Number 1, GetVar "b", Number 0)))))
    let result = Evaluate environment (GetVar "c")
    printfn "Result #4: %d" result

    let lexp = [Add(GetVar "a", Multiply(Number 2, GetVar "b")); If(Number 1, GetVar "b", Number 0)]
    let expr5 = Seq(lexp)
    let result = Evaluate environment expr5
    printfn "Result #5: %d" result

    0 // return an integer exit code


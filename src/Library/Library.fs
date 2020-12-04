module Library

type Expression =
    | Number of int
    | Variable of string
    | Add of Expression * Expression
    | Sub of Expression * Expression
    | Multiply of Expression * Expression
    | Divide of Expression * Expression
    | Neg of Expression
    | If of Expression * Expression * Expression

let rec Evaluate (env:Map<string,int>) exp =
    match exp with
    | Number n -> n
    | Variable id -> env.[id]
    | Add (x, y) -> Evaluate env x + Evaluate env y
    | Sub (x, y) -> Evaluate env x - Evaluate env y
    | Multiply (x, y) -> Evaluate env x * Evaluate env y
    | Divide (x, y) -> Evaluate env x / Evaluate env y
    | Neg (x) -> - Evaluate env x
    | If (cond, eTrue, eFalse) -> if (Evaluate env cond) = 0 then (Evaluate env eFalse) else (Evaluate env eTrue) 
    

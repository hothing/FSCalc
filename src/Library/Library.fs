module Library

//open List

type Expression =
    | Number of int
    | GetVar of string
    | SetVar of string * Expression 
    | Add of Expression * Expression
    | Sub of Expression * Expression
    | Multiply of Expression * Expression
    | Divide of Expression * Expression
    | Neg of Expression
    | If of Expression * Expression * Expression
    | Seq of Expression list

let rec Evaluate (env:Map<string,int>) exp =
    match exp with
    | Number n -> n
    | GetVar id -> env.[id]
    | SetVar (id, value) -> let res = Evaluate env value
                            let nenv = env.Add(id, res) //BUG: it does not work
                            res
    | Add (x, y) -> Evaluate env x + Evaluate env y
    | Sub (x, y) -> Evaluate env x - Evaluate env y
    | Multiply (x, y) -> Evaluate env x * Evaluate env y
    | Divide (x, y) -> Evaluate env x / Evaluate env y
    | Neg (x) -> - Evaluate env x
    | If (cond, eTrue, eFalse) -> if (Evaluate env cond) = 0 then (Evaluate env eFalse) else (Evaluate env eTrue)
    | Seq list -> List.fold (fun a x -> Evaluate env x) 0 list 
    

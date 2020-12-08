module Library

type SimpleExpression =
    | Number of int
    | ValueOf of string
    | Add of SimpleExpression * SimpleExpression
    | Sub of SimpleExpression * SimpleExpression
    | Multiply of SimpleExpression * SimpleExpression
    | Divide of SimpleExpression * SimpleExpression
    | Neg of SimpleExpression
    | ExIf of SimpleExpression * SimpleExpression * SimpleExpression

type Expression =
    | Expr of SimpleExpression
    | Set of string * SimpleExpression
    | Seq of Expression list

type Enviroment = Map<string,int>

// evalSimple : Enviroment -> SimpleExpression -> Int
let rec evalSimple (env : Enviroment)  (exp : SimpleExpression) =
    match exp with
    | Number n -> n
    | ValueOf id -> if env.ContainsKey(id) then
                        env.[id]
                    else
                        failwith (sprintf "Variable %s is not defined" id)
    | Add (x, y) -> evalSimple env x + evalSimple env y
    | Sub (x, y) -> evalSimple env x - evalSimple env y
    | Multiply (x, y) -> evalSimple env x * evalSimple env y
    | Divide (x, y) -> evalSimple env x / evalSimple env y
    | Neg (x) -> - evalSimple env x
    | ExIf (cond, eTrue, eFalse) -> if (evalSimple env cond) = 0 then (evalSimple env eFalse) else (evalSimple env eTrue)


// evaluate : (Enviroment, Expression) -> (Enviroment, Expression)
let rec evaluate ((env, exp) : Enviroment * Expression) =
    match exp with
    | Expr ex -> let r = evalSimple env ex
                 (env, Expr(Number(r)))
    | Set (id, ex) -> let value = evalSimple env ex
                      (env.Add(id, value), Expr(Number(value)))                          
    | Seq elist -> match elist with
                   | head :: tail -> let (nv, ex) = evaluate (env, head)
                                     if not tail.IsEmpty then 
                                         evaluate (nv, Seq(tail))
                                     else
                                         (nv, ex)
                   | [] -> (env, exp)


let getNumber exp =
    match exp with
    | Number value -> value
    | _ -> invalidArg "exp" "is not a Number"

let getResult exp =
    match exp with
    | Expr ex -> getNumber ex 
    | _ -> invalidArg "exp" "is not 'Expr'"


// Brandon Wong
// CPSC 3400
// 5/16/2022
// HW5: Postfix Expression Evaluation
// The code that I am submitting is original code and has not been copied
// from another source. I have written and understand all the code 
// I am submitting.

open System

let rec getNum vars letter: int =
/// Return the int that is paired with provided letter
    match vars with
    | [] -> 0
    | hd :: tl ->
        match hd with
        | (key, num) ->
            if key = letter then num
            else getNum tl letter
  
let push stack hd: int list =
/// Return a stack with the new value pushed on top
    let newStack = hd :: stack
    newStack

let pop stack: int list=
/// Return a stack with the top value popped off
    match stack with
    | [] -> []
    | hd :: tl -> tl

let top stack: int =
/// Return the top value on the stack
    match stack with
    | [] -> 0
    | hd :: tl -> hd

let eval (vars:(string * int) list) (expr:string): int = 
/// Eval function takes a list of (string * int) tuples which is the string int binding
/// variables. The expr string is the expression that will be evaluationed using Postfix
/// Expression Evaluation
    let rec innerEval (vars:(string * int) list) (stack: int list) (expr:string): int =      
    /// Inner recursive function parses the expr string one character at a time. If it encounters
    /// a special character operation, it will perform the according operation to the value(s) on the
    /// stack. If it encounters a space, it will skip the space. If it encounters a number it will push
    /// it onto the stack for operations to be performed on it. Once the expression list is empty, the 
    /// final value on the stack will be the answer
        /// Once the expression list is empty, the final value on the stack will be the answer
        if expr = "" then top stack
        else
            /// Get the next value in the expression to be evaluated and the rest of the expression as strings
            let stringHd = expr.[0].ToString() /// Get the head of the string
            let stringTl = expr.[1..String.length expr].ToString() /// Get the tail of the string
            if stringHd = "+" then
            /// Pop 2 values off the stack, add them, push answer onto the stack
                let a = top stack
                let firstPop = pop stack
                let b = top firstPop
                let secondPop = pop firstPop
                let newValue = a + b
                let newStack = push secondPop newValue
                innerEval vars newStack stringTl
            else if stringHd = "-" then
            /// Pop 2 values off the stack, subtract them, push answer onto the stack
                let a = top stack
                let firstPop = pop stack
                let b = top firstPop
                let secondPop = pop firstPop
                let newValue = b - a
                let newStack = push secondPop newValue
                innerEval vars newStack stringTl
            else if stringHd = "*" then
            /// Pop 2 values off the stack, multiply them, push answer onto the stack
                let a = top stack
                let firstPop = pop stack
                let b = top firstPop
                let secondPop = pop firstPop
                let newValue = a * b
                let newStack = push secondPop newValue
                innerEval vars newStack stringTl
            else if stringHd = "/" then
            /// Pop 2 values off the stack, divided them, push answer onto the stack
                let a = top stack
                let firstPop = pop stack
                let b = top firstPop
                let secondPop = pop firstPop
                let newValue = b / a
                let newStack = push secondPop newValue
                innerEval vars newStack stringTl
            else if stringHd = "$" then
            /// Pop 2 values off the stack, push them back on in reverse order
                let a = top stack
                let firstPop = pop stack
                let b = top firstPop
                let secondPop = pop firstPop
                let firstPush = push secondPop a
                let secondPush = push firstPush b
                innerEval vars secondPush stringTl
            else if stringHd = "@" then
            /// Bind the top value on the stack with the character following the @
                let head = stringTl.[0].ToString()
                let value = top stack
                let newTuple = (head, value)
                let newVars = newTuple :: vars
                let newStringTl = stringTl.[1..String.length expr]
                innerEval newVars stack newStringTl
            else if stringHd = " " then
            /// Skip spaces
                innerEval vars stack stringTl
            else    
            /// Get the number corresponding to the letter and push onto the stack
                let num = getNum vars (string stringHd)
                let newStack = push stack num
                innerEval vars newStack stringTl
    innerEval vars [] expr

// Canvas test cases, expected answers: [7, 2, -2, 49, 51, 0, 64, 5]
let testEval = eval [("a",5);("b",2);("c",9)] 
let exprList = ["ab+"; "cab+-"; "cab+$-"; "ab+cb-*"; "ab+cb-* @d bd+"; "ab-ab*ab+--"; "bbb @q bqbq*****"; "ca- @b bc$-"]
let resultList = List.map testEval exprList
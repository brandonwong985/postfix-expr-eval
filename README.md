# postfix-expr-eval
Practicing F# and functional programming practices.  
This project implements **reverse Polish notation** with a few extra special characters.  
> "In reverse Polish notation, the operators follow their operands; for instance, to add 3 and 4 together, one would write 3 4 + rather than 3 + 4." from https://en.wikipedia.org/wiki/Reverse_Polish_notation  

Implementation rules:  
* When encountering a letter, it should retrieve the value associated with that letter and push it on an operand stack.
* When encountering an arithmetic operator, it should pop two operands off of the stack, do the computation indicated by the operation, and then push the result onto the stack.
* When encountering a $, it should be interpreted as a swap operation, which pops two operands off of the stack and pushes them back on in reverse order.
* When encountering a @, it should read the next character, which will be a letter, then pop the top value from the stack and set that value as the new binding for the letter.
* Any spaces in the string should be skipped.
* When the input string is exhausted, the function should return the top value on the stack.
* You may assume that all input strings are syntactically leagal according to this list of rules.

Example output when running:
```
let testEval = eval [("a",5);("b",2);("c",9)] 
let exprList = ["ab+"; "cab+-"; "cab+$-"; "ab+cb-*"; "ab+cb-* @d bd+"; "ab-ab*ab+--"; "bbb @q bqbq*****"; "ca- @b bc$-"]
let resultList = List.map testEval exprList
val resultList : int list = [7; 2; -2; 49; 51; 0; 64; 5]
```
Explanation walkthrough:
```
"ab+" should return 7 (push 5, push 2, add)
"cab+-" should return 2 (push 9, push 5, push 2, add, subtract)
"cab+$-" should return -2 (push 9, push 5, push 2, add, swap the top two values (9 and 7), subtract)
"ab+cb-*" should return 49 (push 5, push 2, add, push 9, push2, subtract, multiply)
"ab+cb-* @d bd+" should return 51 (push 5, push 2, add, push 9, push2, subtract, multiply, bind 49 to d, push 2, push 49, add)
"ab-ab*ab+--" should return 0 (push 5, push 2, subtract [5-2->3], push 5, push 2, multiply [5*2->10], push 5, push 2, add [5+2->7], subtract [10-7->3], subtract [3-3->0])
"bbb @q bqbq*****" should return 64 (six 2's on the stack before the multiplies)
"ca- @b bc$-" should return 5 ( push 9, push 5, subtract, bind 4 to b, push 4, push 9, swap 4 and 9, subtract)
```

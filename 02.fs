let substitute i value = List.mapi (fun j x -> if j = i then value else x)
let operate opr a b (program : int list) = opr program.[a] program.[b]

// create first-class functions for these operators
let add a b = a + b
let multiply a b = a * b

let rec execute (program : int list) pointer =
  match program.[pointer..] with
  | 1 :: a :: b :: dest :: _ -> execute (substitute dest (operate add a b program) program) (pointer + 4)
  | 2 :: a :: b :: dest :: _ -> execute (substitute dest (operate multiply a b program) program) (pointer + 4)
  | 99 :: _ -> program
  | op :: _ -> invalidArg "program" ([ "unknown opcode"; (string op) ] |> (String.concat " "))
  | _ -> invalidArg "program" "Empty or truncated program"


let printList : (int list -> string) = (List.map string) >> (String.concat ",")

printfn "%s" (printList (execute [1;12;2;3;1;1;2;3;1;3;4;3;1;5;0;3;2;6;1;19;1;19;5;23;2;10;23;27;2;27;13;31;1;10;31;35;1;35;9;39;2;39;13;43;1;43;5;47;1;47;6;51;2;6;51;55;1;5;55;59;2;9;59;63;2;6;63;67;1;13;67;71;1;9;71;75;2;13;75;79;1;79;10;83;2;83;9;87;1;5;87;91;2;91;6;95;2;13;95;99;1;99;5;103;1;103;2;107;1;107;10;0;99;2;0;14;0] 0))

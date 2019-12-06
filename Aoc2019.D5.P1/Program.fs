open System 
open Aoc2019.Utils
open Aoc2019.Utils.Functional

let read source (memory:list<int>) = function | 0 -> memory.[source] | _ -> source
let write target = List.replace target

let mode modeps p = (float)modeps % (10. ** ((float)p)) / (10. ** (float)(p - 1)) |> int
let calcOp f modeps p1 p2 p3 memory = f (read p1 memory (mode modeps 1)) (read p2 memory (mode modeps 2)) |> flip (write p3) memory
let outputOp modeps p1 memory = read p1 memory (mode modeps 1)

let execute input =
    let rec executeRec output pos memory =
        let op::tail = memory |> List.skip pos
        let modeps = op / 100;
        match op % 100 with
        | 99 -> output
        | _  -> let instruction = (op % 10, tail) in match instruction with
            | 4, p1::_ -> (outputOp modeps p1 memory)::output, pos + 2, memory
            | _        -> match instruction with
                | 1, p1::p2::p3::_ -> output, pos + 4, calcOp (+) modeps p1 p2 p3 memory
                | 2, p1::p2::p3::_ -> output, pos + 4, calcOp (*) modeps p1 p2 p3 memory
                | 3, p1::_         -> output, pos + 2, write p1 input memory
            |||> executeRec
    executeRec [] 0 >> Seq.head

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntList >> execute 1 >> sprintf "%A")
    0
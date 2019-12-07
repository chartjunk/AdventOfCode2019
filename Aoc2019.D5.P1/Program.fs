open System 
open Aoc2019.Utils
open Aoc2019.Utils.Functional

let read source (memory:list<int>) = function | 0 -> memory.[source] | _ -> source
let write target = List.replace target

let mode mps p = (float)mps % (10. ** ((float)p)) / (10. ** (float)(p - 1)) |> int
let calcOp f mps p1 p2 p3 memory = let r p i = read p memory (mode mps i) in f (r p1 1) (r p2 2) |> flip (write p3) memory
let outputOp mps p1 memory = read p1 memory (mode mps 1)

let execute input =
    let rec executeRec output pos memory =
        let op::tail = memory |> List.skip pos
        let mps = op / 100;
        match op % 100 with
        | 99 -> output
        | _  -> let instruction = (op % 10, tail) in match instruction with
            | 4, p1::_ -> (outputOp mps p1 memory)::output, pos + 2, memory
            | _        -> match instruction with
                | o&(1|2), p1::p2::p3::_ -> output, pos + 4, calcOp (match o with | 1 -> (+) | _ -> (*)) mps p1 p2 p3 memory
                | 3, p1::_  -> output, pos + 2, write p1 input memory
            |||> executeRec
    executeRec [] 0 >> Seq.head

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntList >> execute 1 >> sprintf "%A")
    0
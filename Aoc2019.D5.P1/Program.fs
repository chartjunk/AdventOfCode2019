open System 
open Aoc2019.Utils
open Aoc2019.Utils.Functional

let read source = function | 0 -> Seq.item source | _ -> fun _ -> source
let write target = List.replace target

let mode mps p = (float)mps % (10. ** ((float)p)) / (10. ** (float)(p - 1)) |> int
let calcOp f mps p1 p2 p3 memory = let r p i = read p (mode mps i) memory in f (r p1 1) (r p2 2) |> flip (write p3) memory
let outputOp mps p1 = read p1 (mode mps 1)

let execute input =
    let rec executeRec output pos memory =
        let op::tail = memory |> List.skip pos
        let mps = op / 100;
        match op % 100 with
        | 99 -> output
        | _  -> match op % 10, tail with
            | o&(1|2), p1::p2::p3::_ -> output, pos + 4, calcOp (match o with | 1 -> (+) | _ -> (*)) mps p1 p2 p3 memory
            | 3, p1::_               -> output, pos + 2, write p1 input memory
            | 4, p1::_               -> (outputOp mps p1 memory)::output, pos + 2, memory
            |||> executeRec
    executeRec [] 0 >> Seq.head

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntList >> execute 1 >> sprintf "%A")
    0
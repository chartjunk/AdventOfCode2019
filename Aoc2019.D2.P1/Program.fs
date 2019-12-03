open System
open Aoc2019.Utils
open Aoc2019.Utils.Functional

let step = 4
let rec runProgram ix program = program |> List.skip ix |> function
    | 99::_         -> program
    | a::b::c::d::_ -> program |> List.replace d (match a with
        | 1 -> (+)
        | 2 -> (*)
        <|| (Pair.map (flip Seq.item program) <| (b, c))) |> runProgram (ix + step)

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntSeq >> Seq.toList >> runProgram 0 >> string)
    0
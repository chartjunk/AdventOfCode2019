open System
open Aoc2019.Utils
open Aoc2019.Utils.Functional
open Aoc2019.D2.P1

let bruteforce wanted memory = (seq {0..99}, seq {0..99}) ||> Seq.allPairs |> Seq.find (flip IntCode.invoke memory >> (=)wanted)

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntList >> bruteforce 19690720 >> (fun (noun, verb) -> 100*noun+verb) >> string)
    0
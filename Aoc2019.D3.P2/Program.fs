open System
open Aoc2019.Utils
open Aoc2019.D3.P1



[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (Wires.read >> Wires.movementPairs >> Seq.re >> string)
    0
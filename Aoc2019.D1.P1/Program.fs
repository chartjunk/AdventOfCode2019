open System
open Aoc2019.Utils
open Aoc2019.D1.P1

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.linesToIntSeq >> Seq.sumBy Fuel.getForMass >> string)
    0
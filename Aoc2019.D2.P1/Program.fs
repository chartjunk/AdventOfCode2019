open System
open Aoc2019.Utils
open Aoc2019.Utils.Functional
open Aoc2019.D2.P1;

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntSeq >> Seq.toList >> IntCode.invoke 12 2 >> string)
    0
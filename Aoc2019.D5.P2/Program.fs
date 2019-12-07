open System 
open Aoc2019.Utils
open Aoc2019.D5.IntCode2

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntList >> execute 5 >> sprintf "%A")
    0
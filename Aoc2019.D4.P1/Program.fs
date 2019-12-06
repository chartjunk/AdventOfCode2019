open System 
open Aoc2019.Utils
open Aoc2019.D4.P1.PasswordCracking

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.set (matches [atLeastOnePair; numbersIncrease] |> Seq.length |> string)
    0 
open System 
open Aoc2019.Utils
open Aoc2019.D4.P1.PasswordCracking

let atLeastOnePairWithoutMoreInARow n = 
    let s = string n
    let rec recurse =
        function
        | r, _, ix when ix >= s.Length           -> match r with | 2 -> true | _ -> false
        | r, c, ix when int s.[ix] <> c && r = 2 -> true
        | x                                      ->
            match x with
            | r, c, ix when int s.[ix] = c -> r + 1, c, ix + 1
            | _, _, ix                     -> 1, int s.[ix], ix + 1
            |> recurse
    recurse (0, -1, 0)

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.set (matches [atLeastOnePairWithoutMoreInARow; numbersIncrease] |> Seq.length |> string)
    0 
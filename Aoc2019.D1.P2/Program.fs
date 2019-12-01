open System
open Aoc2019.Utils
open Aoc2019.D1.P1

let rec getTotalFuelForMass m = Fuel.getForMass m |> function
                                                     | m when m > 0 -> m + getTotalFuelForMass m
                                                     | _ -> 0

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.linesToIntList >> Seq.sumBy getTotalFuelForMass >> string)
    0
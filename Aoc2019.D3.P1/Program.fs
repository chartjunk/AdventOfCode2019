open System 
open Aoc2019.Utils
open Aoc2019.D3.P1.Wires

let manhattanDistance (x:int) (y:int) = Math.Abs(x) + Math.Abs(y)
let nearestNonzeroIntersectionDistance = intersections >> Seq.map(fst >> (<||)manhattanDistance) >> Seq.min

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.linesToStringList >> List.map (String.csvToStringList >> movements) >> nearestNonzeroIntersectionDistance >> string)
    0
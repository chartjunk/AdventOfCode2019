open System 
open Aoc2019.Utils
open Aoc2019.D3.P1.Wires

let distanceOfNearest = Seq.map((<||)manhattanDistance) >> Seq.min

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (readMovements >> intersections >> Seq.map(fst >> fst) >> distanceOfNearest >> string)
    0
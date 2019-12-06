open System 
open Aoc2019.Utils
open Aoc2019.D3.P1.Wires

let manhattanDistance (x:int) (y:int) = Math.Abs(x) + Math.Abs(y)
let distanceToNearest = Seq.map((<||)manhattanDistance) >> Seq.min

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (readMovements >> intersections >> Seq.map(fst >> fst) >> distanceToNearest >> string)
    0
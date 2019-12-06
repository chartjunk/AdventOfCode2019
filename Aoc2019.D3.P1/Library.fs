namespace Aoc2019.D3.P1

//open Aoc2019.Utils
//open Aoc2019.Utils.Functional
//open System

//module Wires =
//    type Dir = Hor=0 | Ver=1
//    let absoluteMovements =
//        Seq.fold (fun (x, y, ms) (next:string) -> let len = int next.[1..] in match next.[0] with
//        | c&'U' | c&'D' -> let ny = (match c with | 'U' -> (+) | _ -> (-)) y len in x, ny, seq { yield! ms; yield (x, y, ny, Dir.Ver) }
//        | c             -> let nx = (match c with | 'R' -> (+) | _ -> (-)) x len in nx, y, seq { yield! ms; yield (y, x, nx, Dir.Hor) }) (0, 0, Seq.empty)
//        >> fun (_, _, ms) -> ms

//    let absoluteMovementsByDir = absoluteMovements >> (fun ms -> ms |> Seq.filter(fth >> (=)Dir.Hor), ms |> Seq.filter(fth >> (=)Dir.Ver))

//    let maybeIntersectionBy (hy:int, hx:int, hnx:int, _) (vx:int, vy:int, vny:int, _) =
//        match Math.Min(hx, hnx) <= vx && Math.Max(hx, hnx) >= vx && Math.Min(vy, vny) <= hy && Math.Max(vy, vny) >= hy, (vx, hy) with
//        | _, (0, 0) -> None
//        | true, _   -> Some (vx, hy)
//        | _         -> None

//    let movementPairs ([w1; w2]:list<list<string>>) =    
//        let (w1h, w1v) = absoluteMovementsByDir w1
//        let (w2h, w2v) = absoluteMovementsByDir w2
//        ((w1h, w2v), (w2h, w1v)) |> Pair.map ((<||)Seq.allPairs) ||> Seq.interleave

//    let read = String.linesToStringList >> List.map String.csvToStringList
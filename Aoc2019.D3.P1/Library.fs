namespace Aoc2019.D3.P1

open Aoc2019.Utils
open Aoc2019.Utils.Functional
open System

module Wires =
    
    let movements =
        List.fold (fun (x, y, ms) (next:string) -> let len = int next.[1..] in match next.[0] with
        | c&'U' | c&'D' -> let ny = (match c with | 'U' -> (+) | _ -> (-)) y len in x, ny, (x, y, ny, 0)::ms
        | c             -> let nx = (match c with | 'R' -> (+) | _ -> (-)) x len in nx, y, (y, x, nx, 1)::ms) (0, 0, [])
        >> thd >> List.rev

    let maybeIntersectionAt (hy:int, hx:int, hnx:int, _) (vx:int, vy:int, vny:int, _) =
        match Math.Min(hx, hnx) <= vx && Math.Max(hx, hnx) >= vx && Math.Min(vy, vny) <= hy && Math.Max(vy, vny) >= hy, (vx, hy) with
        | _, (0, 0) -> None
        | true, p   -> Some (p, Math.Abs(hx - vx) + Math.Abs(hy - vy))
        | _         -> None
    
    let intersections [w1ms; w2ms] = w1ms |> Seq.collecti (fun w1ix w1m -> w2ms |> Seq.choosei (fun w2ix w2m -> match w1m, w2m with
        | _ when fth w1m = fth w2m -> None
        | w1i, w2j                 -> maybeIntersectionAt w1i w2j |> Option.map (fun i -> (i, (w1ix, w2ix)))))

    let readMovements = String.linesToStringList >> List.map (String.csvToStringList >> movements)
open System
open Aoc2019.Utils

let absoluteMovements = Seq.fold (fun (x, y, hs, vs) (next: string) -> let len = int next.[1..] in match next.[0] with
    | c&'U' | c&'D' -> let ny = (match c with | 'U' -> (+) | _ -> (-)) y len in x, ny, hs, vs@[(x, y, ny)]
    | c             -> let nx = (match c with | 'R' -> (+) | _ -> (-)) x len in nx, y, hs@[(y, x, nx)], vs) (0, 0, [], []) >> fun (_, _, hs, vs) -> hs, vs

let maybeIntersectionAt (hy:int, hx:int, hnx:int) (vx:int, vy:int, vny:int) =
    match Math.Min(hx, hnx) <= vx && Math.Max(hx, hnx) >= vx && Math.Min(vy, vny) <= hy && Math.Max(vy, vny) >= hy with
    | true -> Some (vx, hy)
    | _ -> None

let manhattanDistance (x:int) (y:int) = Math.Abs(x) + Math.Abs(y)

let intersections [w1; w2] =
    let (w1h, w1v) = absoluteMovements w1
    let (w2h, w2v) = absoluteMovements w2
    [w1h, w2v; w2h, w1v] |> Seq.collect ((<||)Seq.allPairs) |> Seq.choose ((<||)maybeIntersectionAt)

let nearestNonzeroIntersectionDistance = intersections >> Seq.filter ((<>)(0, 0)) >> Seq.map ((<||)manhattanDistance) >> Seq.min

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.linesToStringList >> List.map String.csvToStringList >> nearestNonzeroIntersectionDistance >> string)
    0
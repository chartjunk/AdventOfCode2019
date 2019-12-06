open System 
open Aoc2019.Utils
open Aoc2019.Utils.Functional

type Dir = Hor=0 | Ver=1
let movements =
    List.fold (fun (x, y, ms) (next:string) -> let len = int next.[1..] in match next.[0] with
    | c&'U' | c&'D' -> let ny = (match c with | 'U' -> (+) | _ -> (-)) y len in x, ny, (x, y, ny, Dir.Ver)::ms
    | c             -> let nx = (match c with | 'R' -> (+) | _ -> (-)) x len in nx, y, (y, x, nx, Dir.Hor)::ms) (0, 0, [])
    >> thd >> List.rev

let maybeIntersectionAt (hy:int, hx:int, hnx:int, _) (vx:int, vy:int, vny:int, _) =
    match Math.Min(hx, hnx) <= vx && Math.Max(hx, hnx) >= vx && Math.Min(vy, vny) <= hy && Math.Max(vy, vny) >= hy, (vx, hy) with
    | _, (0, 0) -> None
    | true, p   -> Some p
    | _         -> None

let manhattanDistance (x:int) (y:int) = Math.Abs(x) + Math.Abs(y)

let intersections [w1ms; w2ms] = w1ms |> List.collecti (fun w1ix w1m -> w2ms |> List.choosei (fun w2ix w2m -> match w1m, w2m with
    | _ when w1ix < w2ix || fth w1m = fth w2m -> None
    | w1i, w2j -> maybeIntersectionAt w1i w2j |> Option.map (fun p -> (p, (w1ix, w2ix)))))

let nearestNonzeroIntersectionDistance = intersections >> Seq.map(fst >> (<||)manhattanDistance) >> Seq.min

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.linesToStringList >> List.map (String.csvToStringList >> movements) >> nearestNonzeroIntersectionDistance >> string)
    0
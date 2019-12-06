open System
open Aoc2019.Utils
open Aoc2019.Utils.Functional
open Aoc2019.D3.P1.Wires

let length (_, i1:int, i2, _) = Math.Abs(i2 - i1)
let cumulativeLength = (<||)Seq.take >> Seq.sumBy length

let firstIntersection (ms & [w1ms; w2ms]) =
    let is = intersections ms |> Seq.toList
    let minIxs f = let min = is |> Seq.minBy(snd >> f) in fst min, fst (snd min), snd (snd min)
    let ((_, w1fresid), w1fw1ix, w1fw2ix), ((_, w2fresid), w2fw1ix, w2fw2ix) = minIxs fst, minIxs snd
    let w1fs = cumulativeLength (w1fw1ix, w1ms) + cumulativeLength (w1fw2ix, w2ms) + w1fresid in
    let w2fs = cumulativeLength (w2fw1ix, w1ms) + cumulativeLength (w2fw2ix, w2ms) + w2fresid in
    Math.Min(w1fs, w2fs)

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (readMovements >> firstIntersection >> string)
    0
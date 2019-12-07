namespace Aoc2019.D5

open System
open Aoc2019.Utils
open Aoc2019.Utils.Functional

module IntCode2 =
    let read source = function | 0 -> Seq.item source | _ -> fun _ -> source
    let write target = List.replace target
    
    let mode ms p = (float)ms % (10. ** ((float)p)) / (10. ** (float)(p - 1)) |> int
    let calcOp f ms p1 p2 p3 mem = let r p i = read p (mode ms i) mem in f (r p1 1) (r p2 2) |> int |> flip (write p3) mem
    let outputOp ms p1 = read p1 (mode ms 1)
    let jumpOp f ms p1 p2 pos mem = let r p i = read p (mode ms i) mem in match r p1 1 |> f with | true -> r p2 2 | _ -> pos + 3
    
    let execute input =
        let rec executeRec output pos mem =
            let op::tail = mem |> List.skip pos
            let ms = op / 100;
            match op % 100 with
            | 99 -> output
            | _  ->
                match op % 10, tail with
                | o&(1|2|7|8), p1::p2::p3::_ ->
                    let f =
                        match o with
                        | 1 -> (+)
                        | 2 -> (*)
                        | o&(7|8) -> fun a b -> Convert.ToInt32(((match o with | 7 -> (<) | 8 -> (=)) a b))
                    output, pos + 4, calcOp f ms p1 p2 p3 mem
                | 3, p1::_                   -> output, pos + 2, write p1 input mem
                | 4, p1::_                   -> (outputOp ms p1 mem)::output, pos + 2, mem
                | o&(5|6), p1::p2::_         -> output, jumpOp (match o with | 5 -> (<>)0 | _ -> (=)0) ms p1 p2 pos mem, mem
                |||> executeRec
        executeRec [] 0 >> Seq.head
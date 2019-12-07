﻿namespace Aoc2019.D5

open System
open Aoc2019.Utils
open Aoc2019.Utils.Functional

module IntCode2 =
    let read source = function | 0 -> Seq.item source | _ -> fun _ -> source
    let write target = List.replace target
    
    let mode mps p = (float)mps % (10. ** ((float)p)) / (10. ** (float)(p - 1)) |> int
    let calcOp f mps p1 p2 p3 memory = let r p i = read p (mode mps i) memory in f (r p1 1) (r p2 2) |> int |> flip (write p3) memory
    let outputOp mps p1 = read p1 (mode mps 1)
    let jumpIfOp f mps p1 p2 pos memory = let r p i = read p (mode mps i) memory in match r p1 1 |> f with | true -> r p2 2 | _ -> pos + 3
    
    let execute input =
        let rec executeRec output pos memory =
            let op::tail = memory |> List.skip pos
            let mps = op / 100;
            match op % 100 with
            | 99 -> output
            | _  ->
                match op % 10, tail with
                | o&(1|2|7|8), p1::p2::p3::_ ->
                    let f =
                        match o with
                        | 1 -> (+) | 2 -> (*) 
                        | o&(7|8) -> fun a b -> Convert.ToInt32(((match o with | 7 -> (<) | 8 -> (=)) a b))
                    output, pos + 4, calcOp f mps p1 p2 p3 memory
                | 3, p1::_                   -> output, pos + 2, write p1 input memory
                | 4, p1::_                   -> (outputOp mps p1 memory)::output, pos + 2, memory
                | o&(5|6), p1::p2::_         -> output, jumpIfOp (match o with | 5 -> (<>)0 | _ -> (=)0) mps p1 p2 pos memory, memory
                |||> executeRec
        executeRec [] 0 >> Seq.head
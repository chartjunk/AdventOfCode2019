namespace Aoc2019.D2.P1

open Aoc2019.Utils
open Aoc2019.Utils.Functional

module IntCode =
    let instructionSize = 4
    let rec execute pointer memory = memory |> List.skip pointer |> function
        | 99::_            -> memory
        | op::i1::i2::o::_ -> memory |> List.replace o (match op with
            | 1 -> (+)
            | 2 -> (*)
            <|| (Pair.map (flip Seq.item memory) <| (i1, i2))) |> execute (pointer + instructionSize)
    let invoke (noun, verb) (o::_::_::tail) = [o;noun;verb]@tail |> execute 0 |> Seq.item 0
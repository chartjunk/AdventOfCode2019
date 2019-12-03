open System
open Aoc2019.Utils
open Aoc2019.Utils.Functional

let instructionSize = 4
let rec execute pointer memory = memory |> List.skip pointer |> function
    | 99::_            -> memory
    | op::i1::i2::o::_ -> memory |> List.replace o (match op with
        | 1 -> (+)
        | 2 -> (*)
        <|| (Pair.map (flip Seq.item memory) <| (i1, i2))) |> execute (pointer + instructionSize)

let getOutput noun verb (o::_::_::tail) = execute 0 [o;noun;verb]@tail |> Seq.item 0

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (String.csvToIntSeq >> Seq.toList >> getOutput 12 2 >> string)
    0
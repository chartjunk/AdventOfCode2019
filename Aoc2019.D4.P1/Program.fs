open System 
open Aoc2019.Utils
open Aoc2019.Utils.Functional

let matches: seq<int> = seq { 234208..765869 } |> Seq.choose(fun it ->
    let maybeIt = MaybeItBuilder(it)
    let pairs = it |> string |> Seq.pairwise
    maybeIt {
        yield pairs |> Seq.tryFind ((<||)(=))
        yield pairs |> Seq.tryFind ((<||)(>)) |> function | None -> Some 0 | _ -> None
    }) 

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.set (matches |> Seq.length |> string)
    0
open System 
open Aoc2019.Utils
open Aoc2019.Utils.Functional

let orbits =
    List.groupBy fst
    >> List.map(fun (k, v) -> k, v |> List.map snd)
    >> dict
    >> (
        fun pcsd ->
            let rec orbitsRec chain =
                (=)
                >> flip Seq.tryFind pcsd.Keys
                >> Option.map (fun p -> (p, pcsd.[p]))
                >> function
                | Some (p, cs) ->
                    cs
                    |> List.collect (fun c -> (p, c)::(List.map(fun (a, _) -> (a, c)) chain)@(orbitsRec ((p, c)::chain) c))
                | None -> []
            pcsd.Keys
            |> Seq.where ((<>) >> flip Seq.all (Seq.collect id pcsd.Values))
            |> Seq.collect (orbitsRec [])
    )

[<EntryPoint; STAThread>]
let main _ =
    Clipboard.apply (
        String.linesToStringList
        >> List.map(String.split ")" >> (fun [p; c] -> (p, c)))
        >> orbits
        >> Seq.length
        >> sprintf "%A"
    )
    0
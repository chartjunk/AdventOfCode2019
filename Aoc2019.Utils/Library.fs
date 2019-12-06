namespace Aoc2019.Utils

open System
open System.Windows.Forms

module Clipboard =
    let get () = Clipboard.GetText()
    let set = Clipboard.SetText
    let apply f = get() |> f |> set

module String =
    let newline = Environment.NewLine
    let toSeq (separator: string) converter (source: string) = source.Split([|separator|], StringSplitOptions.RemoveEmptyEntries) |> Seq.map(converter)
    let linesToSeq converter = toSeq newline converter
    let linesToStringSeq = linesToSeq id
    let linesToStringList = linesToStringSeq >> Seq.toList
    let linesToIntSeq = linesToSeq int
    let csvToStringSeq = toSeq "," id
    let csvToStringList = csvToStringSeq >> Seq.toList
    let csvToIntSeq = toSeq "," int
    let csvToIntList = csvToIntSeq >> Seq.toList

module List =
    let replace ix sub = List.mapi (fun ix0 x -> if ix0 = ix then sub else x)
    let slice ix1 ix2 list = list |> List.skip ix1 |> List.take ix2

module Seq =
    let collecti f = Seq.mapi f >> Seq.collect id
    let choosei f = Seq.mapi f >> Seq.choose id

module Pair =
    let map f (a, b) = (f a, f b)

module Functional =
    let flip f a b = f b a
    let thd (a, b, c) = c
    let fth (a, b, c, d) = d
    type MaybeItBuilder<'T>(it:'T) =
        member this.Yield(x) = x |> Option.map(fun _ -> it) 
        member this.For(m, f) = m |> Option.map f
        member this.Combine(a, b) = match a with
        | Some _ -> b
        | None   -> None
        member this.Delay(f) = f()
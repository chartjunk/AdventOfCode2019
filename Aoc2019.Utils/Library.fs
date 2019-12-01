namespace Aoc2019.Utils

open System
open System.Windows.Forms

module Clipboard =
    let get () = Clipboard.GetText()
    let set = Clipboard.SetText
    let apply f = get() |> f |> set

module String =
    let newline = Environment.NewLine
    let toSeq (separator: string) (source: string) = source.Split([|separator|], StringSplitOptions.RemoveEmptyEntries)
    let linesToList<'T> (converter: string -> 'T) = toSeq newline >> Seq.map(converter) >> Seq.toList
    let linesToStringList = linesToList<string> id
    let linesToIntList = linesToList<int> int

module Functional =
    let flip f a b = f b a
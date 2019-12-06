namespace Aoc2019.D4.P1

open Aoc2019.Utils

module PasswordCracking =
    let matches (conditions: seq<int -> bool>) = seq { 234208..765869 } |> Seq.where(fun i -> conditions |> Seq.all (fun f -> f i))
    let atLeastOnePair = string >> Seq.pairwise >> Seq.exists ((<||)(=))
    let numbersIncrease = string >> Seq.pairwise >> Seq.exists ((<||)(>)) >> (not)
    


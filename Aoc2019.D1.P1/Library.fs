namespace Aoc2019.D1.P1

open Aoc2019.Utils.Functional

module Fuel =
    let getForMass = flip (/) 3 >> (+) -2
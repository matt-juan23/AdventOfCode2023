module AdventOfCodeFs.Day2
open System.IO;
open System.Text.RegularExpressions;

let filename = @"C:\Dev\AdventOfCode2023\AdventOfCode\AdventOfCode\Day2\Input.txt";

let normalise (line:string) = line.Replace(';', ',');

let parseLine (line:string) =
    let hand = Regex.Matches(line, @"(\d+) (\w+)") 
               |> Seq.map (fun m -> (int m.Groups[1].Value, m.Groups[2].Value))
    let game =  Regex.Match(line, @"Game (\d+):") 
               |> fun m -> int m.Groups[1].Value

    (game, hand)

let getScore =
    let maxColours = Map [("red",12); ("green",13); ("blue",14)]
    Seq.filter (snd >> Seq.forall (fun (num, col) -> num <= maxColours[col])) >> Seq.sumBy fst

let getPowerSum =
    let power = Seq.groupBy snd >> Seq.map (snd >> Seq.maxBy fst >> fst) >> Seq.reduce (*)

    Seq.sumBy (snd >> power)


let solvePart1 = File.ReadLines(filename) |> Seq.map (normalise >> parseLine) |> getScore
let solvePart2 = File.ReadLines(filename) |> Seq.map (normalise >> parseLine) |> getPowerSum
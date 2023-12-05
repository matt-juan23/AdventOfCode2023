module AdventOfCodeFs.Day1
open System.IO;
open System

let filename = @"C:\Dev\AdventOfCode2023\AdventOfCode\AdventOfCode\Day1\Input.txt";

let first (s:string) (pattern:string) = s.IndexOf(pattern);
let last (s:string) (pattern:string) = s.LastIndexOf(pattern);

let isDigit (c:char) = Char.IsDigit c;
let atoi (c:char) = int (c - '0');
let substitutes = [("one","1"); ("two","2"); ("three","3"); ("four","4"); ("five","5"); ("six","6"); ("seven","7"); ("eight","8"); ("nine","9")]

let both f g x = (f x, g x)

let findNumbers (line:string) = 
    Seq.filter isDigit line 
    |> both Seq.head Seq.last 
    |> fun (x, y) -> atoi x * 10 + atoi y;

let replace indexFunc selectBy (s:string) = 
    List.map (fun (strNum, num) -> (indexFunc s strNum, num)) substitutes
    |> List.filter (fun (a,_) -> a <> -1)
    |> function
        | [] -> s
        | xs -> selectBy fst xs
                |> fun (i,x) -> s.Insert(i,x)

let findNumbers2 (line:string) = 
    replace first List.minBy line
    |> replace last List.maxBy
    |> findNumbers

let solvePart1 = File.ReadLines(filename) |> Seq.map findNumbers2 |> Seq.sum |> printfn "%A"


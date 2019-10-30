// Learn more about F# at http://fsharp.org

open System

let readVec : int[] = 
    stdin.ReadLine().Split(' ') |> Array.map int

[<EntryPoint>]
let main argv =
    let a = readVec
    printfn "%A" a
    0 // return an integer exit code

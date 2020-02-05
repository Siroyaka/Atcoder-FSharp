open System.Collections.Generic

type Scanner() = class
    let mutable hold = [||]
    let mutable index = 0
    member public this.Next() =
        if hold.Length > index
        then 
            index <- index + 1
            hold.[index - 1]
        else
            let mutable st = stdin.ReadLine()
            while st = "" do st <- stdin.ReadLine()
            hold <- st.Split(' ')
            if hold.Length = 0
            then
                this.Next()
            else
                index <- 1
                hold.[index - 1]
            
    member public this.NextI32() =
        this.Next() |> int

    member public this.ArrayI32(n: int) =
        [|for _ in 0 .. n - 1 -> this.NextI32()|]

    member public this.NextI64() =
        this.Next() |> int64
    
    member public this.ArrayI64(n: int) =
        [|for _ in 0 .. n - 1 -> this.NextI64()|]

    member public this.NextF32() =
        this.Next() |> float32
    
    member public this.ArrayF32(n: int) =
        [|for _ in 0 .. n - 1 -> this.NextF32()|]

    member public this.NextF64() =
        this.Next() |> double
    
    member public this.ArrayF64(n: int) =
        [|for _ in 0 .. n - 1 -> this.NextF64()|]

    member public __.Tuple2Array n (f : int -> 'a list) =
        [|for _ in 0 .. n - 1 -> f 2 |> fun x -> x.[0], x.[1]|]
    
    member public __.Tuple3Array n (f : int -> 'a list) =
        [|for _ in 0 .. n - 1 -> f 3 |> fun x -> x.[0], x.[1], x.[2]|]

    member public __.CountDic n (f : unit -> 'a) =
        let seqs = seq {for _ in 0 .. n - 1 -> f()}
        let dic = new Dictionary<'a, int64>();
        for i in seqs do
            if dic.ContainsKey(i)
            then dic.[i] <- dic.[i] + 1L
            else dic.Add(i, 1L)
        dic

end

[<EntryPoint>]
let main _ =
#if DEBUG
    printfn "debuged"
#endif

    let scanner = Scanner()
    let n = scanner.NextI32()
    let h = scanner.NextI64()
    let arrAB = [|for _ in 0..n-1 -> (scanner.NextI64(), scanner.NextI64())|]
    let maxs = Array.maxBy (fun (x, _) -> x) arrAB |> fst
    let lstB =
        arrAB
        |> Array.map snd
        |> Array.where (fun x -> x > maxs)
        |> Array.toList
        |> List.sortDescending

    let rec f1 target v c =
        function
        | [] -> c, v
        | (x::xs) -> 
            let nx = v + x
            if target <= nx
            then
                (c + 1L), nx
            else
                f1 target nx (c + 1L) xs
    
    let rec f2 target x =
        let y = target / x
        if target % x = 0L then y else y + 1L

    let (a1, v1) = f1 h 0L 0L lstB

    if v1 >= h
    then a1
    else f2 (h - v1) maxs |> (+) a1
    |> printfn "%i"

    0

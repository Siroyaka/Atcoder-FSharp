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

end

[<EntryPoint>]
let main _ =
    #if DEBUG
    printfn "debug"
    #endif
    let scanner = Scanner()
    let s = scanner.Next()
    let n = scanner.NextI32()
    let arrLr = [|for _ in 0 .. n-1 -> (scanner.NextI32(), scanner.NextI32())|]
    
    let change l r t =
        let len = Array.length t
        let newt = t.[l-1..r-1] |> Array.rev
        let fst = if l-1 < 1 then newt else Array.append t.[..l-2] newt
        if r >= len then fst else Array.append fst t.[r..]
        
    arrLr
    |> Array.fold (fun acc (l, r) -> change l r acc) (s.ToCharArray())
    |> Array.map (printf "%c")
    |> ignore
    printfn ""
    0

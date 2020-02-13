
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

    member public this.IArrayI32(n: int) =
        [|for i in 0 .. n - 1 -> this.NextI32(), i|]

    member public this.NextI64() =
        this.Next() |> int64
    
    member public this.ArrayI64(n: int) =
        [|for _ in 0 .. n - 1 -> this.NextI64()|]

    member public this.IArrayI64(n: int) =
        [|for i in 0 .. n - 1 -> this.NextI64(), i|]

    member public this.NextF32() =
        this.Next() |> float32
    
    member public this.ArrayF32(n: int) =
        [|for _ in 0 .. n - 1 -> this.NextF32()|]

    member public this.IArrayF32(n: int) =
        [|for i in 0 .. n - 1 -> this.NextF32(), i|]

    member public this.NextF64() =
        this.Next() |> double
    
    member public this.ArrayF64(n: int) =
        [|for _ in 0 .. n - 1 -> this.NextF64()|]

    member public this.IArrayF64(n: int) =
        [|for i in 0 .. n - 1 -> this.NextF64(), i|]

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
    let csf = [|for _ in 0..n-2 -> scanner.NextI64(), scanner.NextI64(), scanner.NextI64()|]
    let func1 i (arr: (int64 * int64 * int64) array) =
        let (c, s, _) = arr.[i]
        let next = c + s
        let rec func2 ns =
            function
            | x when x = n - 1 -> ns
            | x ->
                let (cc, ss, ff) = arr.[x]
                let iss = if ss > ns then ss else ns
                let iff = if iss % ff = 0L then iss else iss + ff - iss % ff
                func2 (iff + cc) (x + 1)
        func2 next (i + 1)

    if n <> 1 then
        [|for i in 0..n-2 -> func1 i csf|]
        |> Array.iter (printfn "%i")

    printfn "0"

    0

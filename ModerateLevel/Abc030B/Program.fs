
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
    let h = scanner.NextF32() % 12.0f
    let m = scanner.NextF32()
    let fh h m = h * 30.0f + m * 0.5f
    let fm m = m * 6.0f
    let hp, mp = (fh h m, fm m)
    abs (hp - mp)
    |> fun x -> (x, 360.0f - x)
    |> fun (x, y) -> min x y
    |> printfn "%f"

    0

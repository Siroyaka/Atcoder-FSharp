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

type Ordering =
    | LT = -1
    | EQ = 0
    | GT = 1

[<EntryPoint>]
let main _ =
    let scanner = Scanner()
    let n = scanner.NextI32()
    let arr = scanner.ArrayI64(n)
    let order a b = if a > b then Ordering.GT else if a < b then Ordering.LT else Ordering.EQ
    let ff b bb = if b = bb || b = Ordering.EQ || bb = Ordering.EQ then 0L else 1L
    let rec f b c =
        function
        | [] -> c
        | (x,y)::xs -> 
            let ord = order x y
            if ord = Ordering.EQ
            then
                f b c xs
            else
                let ad = ff b ord
                f (if ad = 1L then Ordering.EQ else ord) (c + ad) xs
 
    if (Array.length arr) < 2
    then 1L
    else
        let pairs = Array.pairwise arr
        let (x, y) = pairs.[0]
        Array.toList pairs
        |> f (order x y) 1L
    |> printfn "%i"
    
    0

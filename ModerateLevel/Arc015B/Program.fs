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
        [for _ in 0 .. n - 1 -> this.NextI32()]

    member public this.NextI64() =
        this.Next() |> int64
    
    member public this.ArrayI64(n: int) =
        [for _ in 0 .. n - 1 -> this.NextI64()]

    member public this.NextF32() =
        this.Next() |> float32
    
    member public this.ArrayF32(n: int) =
        [for _ in 0 .. n - 1 -> this.NextF32()]

    member public this.NextF64() =
        this.Next() |> double
    
    member public this.ArrayF64(n: int) =
        [for _ in 0 .. n - 1 -> this.NextF64()]

    member public __.Tuple2List n (f : int -> 'a list) =
        [for _ in 0 .. n - 1 -> f 2 |> fun x -> x.[0], x.[1]]
    
    member public __.Tuple3List n (f : int -> 'a list) =
        [for _ in 0 .. n - 1 -> f 3 |> fun x -> x.[0], x.[1], x.[2]]

end

[<EntryPoint>]
let main _ =
    let scanner = Scanner()
    let n = scanner.NextI32()
    let m = scanner.Tuple2List n scanner.ArrayF64
    let mutable a, b, c, d, e, f = 0, 0, 0, 0, 0, 0

    for (mx, mn) in m do
        a <- if mx >= 35.0 then a + 1 else a
        b <- if mx >= 30.0 && mx < 35.0 then b + 1 else b
        c <- if mx >= 25.0 && mx < 30.0 then c + 1 else c
        d <- if mn >= 25.0 then d + 1 else d
        e <- if mx >= 0.0 && mn < 0.0 then e + 1 else e
        f <- if mx < 0.0 then f + 1 else f

    printfn "%i %i %i %i %i %i" a b c d e f

    0
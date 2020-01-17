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
    let a = scanner.Next()
    let b = scanner.Next()

    let lenComparison f lena lenb =
        if f lena > f lenb then
            (true, "GREATER")
        else if f lena < f lenb then
            (true, "LESS")
        else
            (false, "")

    let rec intComparison (aa:string) (bb:string) ind len =
        let x = int aa.[ind]
        let y = int bb.[ind]
        if x <> y then
            if x > y then "GREATER" else "LESS"
        else
            if ind + 1 = len then
                "EQUAL"
            else
                intComparison aa bb (ind + 1) len

    match lenComparison String.length a b with
        | (x, y) when x -> y
        | (_, _) -> intComparison a b 0 (String.length a)
    |> printfn "%s"

    0


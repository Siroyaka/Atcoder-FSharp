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
end

[<EntryPoint>]
let main _ =
    let sc = Scanner()
    let n, s, t = (sc.NextI32(), sc.NextI32(), sc.NextI32())
    let a = sc.ArrayI32(n)

    List.scan (+) 0 a
    |> List.where(fun x -> x >= s && x <= t)
    |> List.length
    |> printfn "%i"
    0

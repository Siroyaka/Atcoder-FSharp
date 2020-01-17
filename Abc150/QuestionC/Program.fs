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

let lexiperm a =
    let len = Array.length a
    let rec getPivot ind =
        let scoap = len - ind - 1
        if scoap < 0 then -1
        else if a.[scoap + 1] > a.[scoap] then scoap
        else getPivot (ind + 1)
    let pivot = getPivot 1
    if pivot < 0 then a
    else
    let before = a.[0..pivot]
    let originsuf = a.[pivot+1..]
    let suffix = 
        originsuf
        |> Array.filter(fun x -> x > a.[pivot])
    let suffixmin = suffix |> Array.min
    let minindex = originsuf |> Array.findIndex(fun x -> x = suffixmin)
    let hold = originsuf.[minindex]
    originsuf.[minindex] <- before.[pivot]
    before.[pivot] <- hold
    originsuf
    |> Array.sort
    |> Array.append before

[<EntryPoint>]
let main _ =
    let scanner = Scanner()
    let n = scanner.NextI32()
    let p = scanner.ArrayI32(n) |> List.toArray
    let q = scanner.ArrayI32(n) |> List.toArray
    let mutable pind = 0
    let mutable qind = 0
    let mutable t = Array.sort p
    let kai = Array.fold ( * ) 1 [|1..n|]

    for i in 1..kai do
        if t = p then pind <- i
        if t = q then qind <- i
        t <- lexiperm t
    
    pind - qind
    |> abs
    |> printfn "%i" 
    0

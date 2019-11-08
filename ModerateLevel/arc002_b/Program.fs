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
            hold <- st.Split('/')
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
    let y = sc.NextI32()
    let m = sc.NextI32()
    let d = sc.NextI32()
    let nextDay year month day =
        let uruu =
            match year with
            | x when x % 100 = 0 -> x % 400 = 0
            | x -> x % 4 = 0
        let daymax =
            match month with
            | 2 -> if uruu then 29 else 28
            | 4 | 6 | 9 | 11 -> 30
            | _ -> 31
        let monthAdd () =
            if month = 12 then
                (year + 1), 1
            else
                year, (month + 1)
        if day = daymax then
            let a, b = monthAdd ()
            a, b, 1
        else
            year, month, (day + 1)

    let rec daysDiv year month day =
        if month * day |> (fun x -> year % x = 0) then year, month, day
        else nextDay year month day |> fun (yr, mt, dy) -> daysDiv yr mt dy
    let addZero v =
        if v < 10 then "0" + v.ToString()
        else v.ToString()
    let y, m, d = daysDiv y m d
    let m = addZero m
    let d = addZero d

    printfn "%i/%s/%s" y m d
    0

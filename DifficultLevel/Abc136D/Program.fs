
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
    let iArr = scanner.Next().ToCharArray()
    let leng = Array.length iArr
    let ans = Array.zeroCreate leng

    let fans countR countL ind =
        let lInd = ind - countL
        let rInd = lInd - 1
        let rVal = (countR - countR / 2) + (countL / 2)
        let lVal = countR + countL - rVal
        Array.set ans rInd rVal
        Array.set ans lInd lVal

    let rec fl countR countL ind =
        Array.tryItem ind iArr
        |> function
            | None ->
                fans countR countL ind
                -1
            | Some(x) ->
                match x with
                | 'R' ->
                    fans countR countL ind
                    ind
                | _ -> fl countR (countL+1) (ind+1)
    
    let rec fR countR ind =
        let mutable nxcR = countR + 1
        let nxInd =
            if Array.item ind iArr = 'R'
            then
                ind + 1
            else
                nxcR <- 0
                fl countR 0 ind
        if nxInd = -1
        then
            0
        else
            fR nxcR nxInd

    fR 0 0 |> ignore

    ans
    |> Array.map string
    |> String.concat " "
    |> printfn "%s"

    0

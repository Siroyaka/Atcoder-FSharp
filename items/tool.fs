/// ユークリッドの互除法
let uc a b =
    let rec sub l s = 
        if s = 0 then l else l % s |> sub s
    if a > b then sub a b else sub b a


// ランレングス変換
// [|1;1;1;2;2;3;1;1;2;1;3;3;3|] ->
// [|(1, 3); (2, 2); (3, 1); (1, 2); (2, 1); (1, 1); (3, 3)|]
let toRunLength arr =
    let f ch array =
        let last = Array.tryLast array
        match last with
            | (Some(x, i)) when x = ch ->
                Array.set array ((Array.length array) - 1) (x, i + 1)
                array
            | _ -> Array.singleton (ch, 1) |> Array.append array
    arr
    |> Array.fold (fun acum x -> f x acum) [||]
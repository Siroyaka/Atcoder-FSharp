/// ユークリッドの互除法
let uc a b =
    let rec sub l s = 
        if s = 0 then l else l % s |> sub s
    if a > b then sub a b else sub b a

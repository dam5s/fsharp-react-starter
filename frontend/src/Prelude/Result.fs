[<RequireQualifiedAccess>]
module Result

let apply (func: Result<'a -> 'b, 'c>) (value: Result<'a, 'c>): Result<'b, 'c> =
    match func, value with
    | Ok f, Ok a -> Ok (f a)
    | _, Error e -> Error e
    | Error e, _ -> Error e

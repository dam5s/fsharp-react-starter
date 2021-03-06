[<AutoOpen>]
module Frontend.AsyncResult

type AsyncResult<'a, 'b> = Async<Result<'a, 'b>>

[<RequireQualifiedAccess>]
module AsyncResult =
    let map mapping result =
        async {
            match! result with
            | Ok a -> return Ok (mapping a)
            | Error b -> return Error b
        }

    let bind mapping result =
        async {
            match! result with
            | Ok a -> return! mapping a
            | Error b -> return Error b
        }

    let mapError mapping result =
        async {
            match! result with
            | Ok a -> return Ok a
            | Error b -> return Error (mapping b)
        }

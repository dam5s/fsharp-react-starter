[<RequireQualifiedAccess>]
module Frontend.Option
    let toResult err opt =
        match opt with
        | Some o -> Ok o
        | None -> Error err

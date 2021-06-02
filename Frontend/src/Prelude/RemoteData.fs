[<AutoOpen>]
module Frontend.RemoteData

type RemoteData<'a, 'b> =
    | NotLoaded
    | Loading
    | Loaded of 'a
    | Failure of 'b
    | Refreshing of 'a

[<RequireQualifiedAccess>]
module RemoteData =

    let startLoading data =
        match data with
        | NotLoaded -> Loading
        | Loading -> Loading
        | Loaded a -> Refreshing a
        | Failure _ -> Loading
        | Refreshing a -> Refreshing a

    let ofResult result =
        match result with
        | Ok a -> Loaded a
        | Error b -> Failure b

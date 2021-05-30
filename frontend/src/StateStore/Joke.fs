[<RequireQualifiedAccess>]
module StateStore.Joke

open Networking

type State =
    { Data: RemoteData<string, Http.Error> }

let init =
    { Data = NotLoaded }

type Action =
    | StartLoading
    | FinishLoading of Result<string, Http.Error>

let reducer (action: Action) (state: State): State =
    match action with
    | StartLoading -> { Data = RemoteData.startLoading state.Data }
    | FinishLoading result -> { Data = RemoteData.ofResult result }

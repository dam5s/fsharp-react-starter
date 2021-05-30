[<RequireQualifiedAccess>]
module StateStore.Effects

open Prelude.Redux
open AppState
open Networking

type Effect =
    | LoadJoke

let private loadJoke dispatch state =
    dispatch Joke.StartLoading

    async {
        let! result = JokeApi.random()
        dispatch (Joke.FinishLoading result)
    }

let middleware (dispatch: Dispatch) (state: AppState) (effect: Effect) =
    match effect with
    | LoadJoke -> loadJoke dispatch state |> Async.Start

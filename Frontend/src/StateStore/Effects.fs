[<RequireQualifiedAccess>]
module Frontend.StateStore.Effects

open Frontend
open Frontend.Redux
open Frontend.Networking
open AppState

type Effect =
    | LoadInitialJoke
    | RefreshJoke

let private doNothing =
    async { return () }

let private loadJoke dispatch state =
    dispatch Joke.StartLoading

    async {
        let! result = JokeApi.random()
        dispatch (Joke.FinishLoading result)
    }

let private loadInitialJoke dispatch (state: AppState) =
    if state.Joke.Data = NotLoaded
    then loadJoke dispatch state
    else doNothing

let middleware (dispatch: Dispatch) (state: AppState) (effect: Effect) =
    match effect with
    | LoadInitialJoke -> loadInitialJoke dispatch state
    | RefreshJoke -> loadJoke dispatch state
    |> Async.StartImmediate

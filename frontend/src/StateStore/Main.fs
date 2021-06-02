module StateStore.Main

open Redux
open AppState

let private middleware (dispatch: Dispatch) (state: AppState) (next: Dispatch) (action: Action) =
    match action with
    | :? Effects.Effect as effect -> Effects.middleware dispatch state effect
    | _ -> next action

let private reducer (obj: Action) (state: AppState) =
    match obj with
    | :? Navigation.Action as action -> { state with Navigation = Navigation.reducer action state.Navigation }
    | :? Counter.Action as action -> { state with Counter = Counter.reducer action state.Counter }
    | :? Joke.Action as action -> { state with Joke = Joke.reducer action state.Joke }
    | _ -> state

let private initialState = { Navigation = Navigation.init
                             Counter = Counter.init
                             Joke = Joke.init }

let stateStore _ = StateStore(initialState, middleware, reducer)

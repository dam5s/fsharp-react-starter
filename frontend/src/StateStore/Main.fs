module StateStore.Main

open Redux
open AppState

let middleware (dispatch: Dispatch) (state: AppState) (next: Dispatch) (action: Action) =
    match action with
    | :? Effects.Effect as effect -> Effects.middleware dispatch state effect
    | _ -> next action

let reducer (obj: Action) (state: AppState) =
    match obj with
    | :? Page.Action as action -> { state with Page = Page.reducer action state.Page }
    | :? Counter.Action as action -> { state with Counter = Counter.reducer action state.Counter }
    | :? Joke.Action as action -> { state with Joke = Joke.reducer action state.Joke }
    | _ -> state

let private initialState = { Page = Page.init
                             Counter = Counter.init
                             Joke = Joke.init }

let stateStore _ = StateStore(initialState, middleware, reducer)

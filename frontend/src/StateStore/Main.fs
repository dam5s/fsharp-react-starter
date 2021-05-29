module StateStore.Main

open Prelude.Redux
open Page
open Counter

type AppState = { Page: Page; Counter: int }

let reducer (obj: Action) (state: AppState) =
    match obj with
    | :? Page.Action as action -> { state with Page = Page.reducer action state.Page }
    | :? Counter.Action as action -> { state with Counter = Counter.reducer action state.Counter }
    | _ -> state

let private initialState = { Page = Home; Counter = 0 }

let stateStore = StateStore(initialState, reducer)

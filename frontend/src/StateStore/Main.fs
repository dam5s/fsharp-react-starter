module StateStore.Main

open Prelude.Redux
open Page

type State = { Page: Page }

let reducer (action: Action) (state: State) =
    match action with
    | :? Page.Action as pageAction -> { Page = Page.reducer pageAction state.Page }
    | _ -> state

let initialState = { Page = HomePage }

let stateStore = StateStore(initialState, reducer)

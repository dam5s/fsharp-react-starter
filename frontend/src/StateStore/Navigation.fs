[<RequireQualifiedAccess>]
module StateStore.Navigation

type Page =
    | Home
    | Counter
    | Joke

type State =
    { Page: Page option }

let init: State =
    { Page = Some Home }

type Action =
    | Navigate of string list

let href page =
    match page with
    | Home -> "/"
    | Counter -> "/counter"
    | Joke -> "/joke"

let reducer (Navigate newRoute) (_: State): State =
    match newRoute with
    | [] -> { Page = Some Home }
    | ["counter"] -> { Page = Some Counter }
    | ["joke"] -> { Page = Some Joke }
    | _ -> { Page = None }

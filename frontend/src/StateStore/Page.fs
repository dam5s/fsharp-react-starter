[<RequireQualifiedAccess>]
module StateStore.Page

type State =
    | Home
    | Counter
    | Joke

let init =
    Home

type Action =
    | Change of State

let reducer (Change newPage) (_: State) =
    newPage

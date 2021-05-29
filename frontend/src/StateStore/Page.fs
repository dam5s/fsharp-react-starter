module StateStore.Page

type Page =
    | HomePage
    | CounterPage

[<RequireQualifiedAccess>]
module Page =
    type Action =
        | Change of Page

    let reducer (Change newPage) (_: Page) =
        newPage

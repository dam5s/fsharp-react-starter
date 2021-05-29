module StateStore.Page

type Page =
    | HomePage
    | CounterPage

[<RequireQualifiedAccess>]
module Page =
    type Action =
        | ChangePage of Page

    let reducer (ChangePage newPage) (_: Page) =
        newPage

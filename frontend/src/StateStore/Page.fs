module StateStore.Page

type Page =
    | Home
    | Counter

[<RequireQualifiedAccess>]
module Page =
    type Action =
        | Change of Page

    let reducer (Change newPage) (_: Page) =
        newPage

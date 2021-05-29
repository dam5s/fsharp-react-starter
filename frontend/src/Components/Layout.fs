module Components.Layout

open Browser.Types
open Feliz
open StateStore.Page
open StoreProvider
open Counter

[<ReactComponent>]
let HomePage () =
    Html.main [ Html.h1 "Home" ]

[<ReactComponent>]
let Layout () =
    let dispatch = useDispatch ()
    let page = useSelector (fun s -> s.Page)

    let changePage page (event: MouseEvent) =
        event.preventDefault ()
        dispatch (Page.Change page)

    let pageContent =
        match page with
        | Home -> HomePage ()
        | Counter -> CounterPage ()

    let link (text: string) href onClick =
        Html.a [ prop.href href; prop.text text; prop.onClick onClick ]

    Html.div [
        Html.nav [
            link "Home" "/#/home" (changePage Page.Home)
            link "Counter" "/#/counter" (changePage Page.Counter)
        ]
        pageContent
    ]

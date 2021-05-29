module Components.Layout

open Browser.Types
open Feliz
open StateStore.Page
open StoreProvider
open Counter

[<ReactComponent>]
let Layout () =
    let dispatch = useDispatch ()
    let page = useSelector (fun s -> s.Page)

    let changePage page (event: MouseEvent) =
        event.preventDefault ()
        dispatch (Page.Change page)

    let counterContent _ =
        [ Html.h1 "Counter"
          Html.p "You can click on this react counter"
          Counter() ]

    let pageContent =
        match page with
        | HomePage -> [ Html.h1 "Home" ]
        | CounterPage -> counterContent ()

    let link (text: string) href onClick =
        Html.a [ prop.href href; prop.text text; prop.onClick onClick ]

    Html.div [
        Html.nav [
            link "Home" "/#/home" (changePage HomePage)
            link "Counter" "/#/counter" (changePage CounterPage)
        ]
        Html.main pageContent
    ]

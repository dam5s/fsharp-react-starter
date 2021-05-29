module Components.Layout

open Feliz
open StateStore.Main
open StateStore.Page
open Components.Counter

[<ReactComponent>]
let Layout () =
    let changePage page _ =
        stateStore.Dispatch(Page.ChangePage page)

    let page =
        stateStore.Select(fun s -> s.Page)

    let counterContent _ =
        [ Html.h1 "Counter"
          Html.p "You can click on this react counter"
          Counter() ]

    let pageContent =
        match page with
        | HomePage -> [ Html.h1 "Home" ]
        | CounterPage -> counterContent ()

    Html.div [
        Html.nav [ Html.a [ prop.href "#"
                            prop.text "Home"
                            prop.onClick (changePage HomePage) ]
                   Html.a [ prop.href "#"
                            prop.text "Counter"
                            prop.onClick (changePage CounterPage) ] ]
        Html.main pageContent
    ]

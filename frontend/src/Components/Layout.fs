module Components.Layout

open Browser.Types
open Feliz
open StateStore
open StoreProvider
open CounterPage
open JokePage

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
        | Page.Home -> HomePage ()
        | Page.Counter -> CounterPage ()
        | Page.Joke -> JokePage ()

    let link (text: string) href onClick =
        Html.a [ prop.href href; prop.text text; prop.onClick onClick ]

    Html.div [
        Html.nav [
            link "Home" "/#/home" (changePage Page.Home)
            link "Counter" "/#/counter" (changePage Page.Counter)
            link "Joke" "/#/joke" (changePage Page.Joke)
        ]
        pageContent
    ]

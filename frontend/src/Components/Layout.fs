module Components.Layout

open Feliz
open Feliz.Router
open StateStore
open StoreProvider
open CounterPage
open JokePage
open PageLink

[<ReactComponent>]
let HomePage () =
    Html.main [ Html.h1 "Home" ]

let PageNotFound () =
    Html.main [ Html.p "This is not the page you are looking for."
                Html.p [ Html.text "Go "; PageLink Navigation.Home ] ]

[<ReactComponent>]
let Layout () =
    let dispatch = useDispatch ()
    let page = useSelector (fun s -> s.Navigation.Page)

    React.useEffectOnce (Router.currentPath >> Navigation.Navigate >> dispatch)

    let pageContent =
        match page with
        | Some Navigation.Home -> HomePage ()
        | Some Navigation.Counter -> CounterPage ()
        | Some Navigation.Joke -> JokePage ()
        | None -> PageNotFound ()

    React.router [
        router.pathMode
        router.onUrlChanged (Navigation.Navigate >> dispatch)
        router.children [
            Html.nav [
                PageLink Navigation.Home
                PageLink Navigation.Counter
                PageLink Navigation.Joke
            ]
            pageContent
        ]
    ]

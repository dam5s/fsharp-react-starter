module Components.JokePage

open Feliz
open Components.StoreProvider
open Networking
open StateStore

[<ReactComponent>]
let JokePage () =
    let dispatch = useDispatch ()
    let data = useSelector (fun s -> s.Joke.Data)

    React.useEffectOnce (fun _ -> dispatch Effects.LoadInitialJoke)

    let text =
        match data with
        | NotLoaded -> ""
        | Loading -> ""
        | Loaded joke -> joke
        | Failure (Http.ConnectionError _) -> "There was a connection error, please check your internet and try again"
        | Failure _ -> "There was an error loading the joke, please try again later"
        | Refreshing joke -> joke

    let refreshJoke _ = dispatch Effects.RefreshJoke

    let refreshButton (text: string) disabled =
        Html.button [ prop.text text; prop.onClick refreshJoke; prop.disabled disabled ]

    let button =
        match data with
        | NotLoaded -> refreshButton "..." true
        | Loading -> refreshButton "Loading..." true
        | Loaded _ -> refreshButton "Refresh" false
        | Failure _ -> refreshButton "Try Again" false
        | Refreshing _ -> refreshButton "Refresh" true

    Html.main [
        Html.h1 "Joke"
        Html.blockquote [ prop.dangerouslySetInnerHTML text ]
        Html.footer [ Html.text "This joke courtesy of "
                      Html.a [ prop.text "The Internet Chuck Norris Database.";
                               prop.href "https://www.icndb.com/" ]
        ]
        button
    ]

module Components.JokePage

open Feliz
open Components.StoreProvider
open Networking
open StateStore

[<ReactComponent>]
let JokePage () =
    let dispatch = useDispatch ()
    let data = useSelector (fun s -> s.Joke.Data)

    React.useEffectOnce (fun _ -> dispatch Effects.LoadJoke)

    let text =
        match data with
        | NotLoaded -> ""
        | Loading -> "Loading..."
        | Loaded joke -> joke
        | Failure (Http.ConnectionError _) -> "There was a connection error, please check your internet and try again"
        | Failure _ -> "There was an error loading the joke, please try again later"
        | Refreshing joke -> joke

    Html.main [
        Html.h1 "Joke"
        Html.p text
    ]

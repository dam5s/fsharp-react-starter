module Components.JokePage

open Feliz
open Prelude.RemoteData
open Components.StoreProvider
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
        | Failure _ -> "There was an error loading the joke"
        | Refreshing joke -> joke

    Html.main [
        Html.h1 "Joke"
        Html.p text
    ]

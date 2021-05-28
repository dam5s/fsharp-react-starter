module App

open Browser.Dom
open Feliz

[<ReactComponent>]
let Counter() =
    let (count, setCount) = React.useState(0)

    Html.button [
        prop.text $"Clicked %d{count} times"
        prop.onClick (fun _ -> setCount(count + 1))
    ]

[<ReactComponent>]
let App() =
    Html.main [
        Html.p "Fable is running"
        Html.p "You can click on this react counter"
        Counter()
    ]

ReactDOM.render(App(), document.getElementById "root")

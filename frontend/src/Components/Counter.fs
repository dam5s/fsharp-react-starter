module Components.Counter

open Feliz

[<ReactComponent>]
let Counter () =
    let (count, setCount) = React.useState (0)

    Html.button [ prop.text $"Clicked %d{count} times"
                  prop.onClick (fun _ -> setCount (count + 1)) ]

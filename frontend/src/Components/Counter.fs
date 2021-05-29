module Components.Counter

open Feliz
open Components.StoreProvider
open StateStore.Counter

[<ReactComponent>]
let Counter () =
    let dispatch = useDispatch ()
    let count = useSelector (fun s -> s.Counter)
    let onClick _ = dispatch Counter.Increment

    Html.button [ prop.text $"Clicked %d{count} times"
                  prop.onClick onClick ]

[<ReactComponent>]
let CounterPage () =
    Html.main [
        Html.h1 "Counter"
        Html.p "You can click on this counter"
        Counter()
    ]

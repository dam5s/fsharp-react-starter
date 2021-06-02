[<RequireQualifiedAccess>]
module Frontend.StateStore.Counter

type State = int

let init = 0

type Action = Increment

let reducer Increment count =
    count + 1

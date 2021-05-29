module StateStore.Counter

[<RequireQualifiedAccess>]
module Counter =
    type Action = Increment

    let reducer Increment count =
        count + 1

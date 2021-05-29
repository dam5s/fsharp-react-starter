module Prelude.Redux

type private Subscription<'state> =
    abstract member Notify : 'state -> unit

type private Subscriber<'state, 'transformed when 'transformed: equality>
    (
        initialState: 'state,
        transform: 'state -> 'transformed,
        onChange: 'transformed -> unit
    ) =

    let mutable previousState = transform initialState
    let transform = transform
    let onChange = onChange

    interface Subscription<'state> with
        member this.Notify(state: 'state) =
            let transformed = transform state

            if transformed <> previousState then
                previousState <- transformed
                onChange transformed

type Action = obj

type StateStore<'state when 'state : equality>
    (
        state: 'state,
        reducer: Action -> 'state -> 'state
    ) =

    let mutable state = state
    let mutable subs : Subscription<'state> list = []

    member this.GetState () = state

    member this.Subscribe (transform: 'state -> 'a, onChange: 'a -> unit) =
        let subscriber = Subscriber(state, transform, onChange)
        let subscription = subscriber :> Subscription<'state>
        subs <- subscription :: subs

    member this.Dispatch (action: Action) =
        state <- reducer action state

        for s in subs do
            s.Notify state

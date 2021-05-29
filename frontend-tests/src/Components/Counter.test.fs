module Components.CounterTest

open Fable.Jester
open Fable.ReactTestingLibrary
open StateStore.Main
open Components.StoreProvider
open Components.Counter

Jest.describe("Counter", fun () ->
    Jest.test("incrementing the counter", fun () ->
        let store = stateStore()

        RTL.render (StoreProvider store (Counter())) |> ignore

        RTL.screen.getByText("Clicked 0 times").click()
        RTL.screen.getByText("Clicked 1 times").click()
        RTL.screen.getByText("Clicked 2 times").click()
    )
)

module Components.CounterTest

open Fable.Jester
open Fable.ReactTestingLibrary
open StateStore.Main
open Components.StoreProvider
open Components.CounterPage

Jest.describe("Counter", fun () ->
    Jest.test("incrementing the counter", fun () ->
        let store = stateStore()

        let rendered = RTL.render (StoreProvider store (Counter()))

        rendered.getByText("Clicked 0 times").click()
        rendered.getByText("Clicked 1 times").click()
        rendered.getByText("Clicked 2 times").click()
    )
)

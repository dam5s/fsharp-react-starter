module Frontend.Tests.Components.JokeTest

open Fable.Jester
open Fable.ReactTestingLibrary

open Frontend.Tests.Support
open Frontend.Tests.Support.MockWebServer

open Frontend.StateStore.Main
open Frontend.Components.StoreProvider
open Frontend.Components.JokePage

Jest.describe("JokePage", fun () ->

    let mutable server = MockWebServer.create ()

    Jest.beforeEach(fun _ ->
        server <- MockWebServer.create ()
        server.Start()
    )

    Jest.afterEach(fun _ ->
        server.Stop()
    )

    let jokeJson = {| text = "hello world" |} :> obj
    let emptyJson = {||} :> obj

    Jest.test("loading a random joke", promise {
        TestEnv.setApiUrl (server.Url "")
        server.Stub 200 jokeJson

        let store = stateStore()
        let page = RTL.render(StoreProvider store (JokePage()))
        let find = page.findByText("hello world")

        do! Jest.expect(find).resolves.toBeTruthy()
    })

    Jest.test("on decode error", promise {
        TestEnv.setApiUrl (server.Url "")
        server.Stub 200 emptyJson

        let store = stateStore()
        let page = RTL.render(StoreProvider store (JokePage()))
        let find = page.findByText("There was an error loading the joke, please try again later")

        do! Jest.expect(find).resolves.toBeTruthy()
    })

    Jest.test("on server error", promise {
        TestEnv.setApiUrl (server.Url "")
        server.Stub 500 emptyJson

        let store = stateStore()
        let page = RTL.render(StoreProvider store (JokePage()))
        let find = page.findByText("There was an error loading the joke, please try again later")

        do! Jest.expect(find).resolves.toBeTruthy()
    })
)

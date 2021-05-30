module Components.JokeTest

open Fable.Jester
open Fable.ReactTestingLibrary
open TestingSupport
open TestingSupport.MockWebServer
open StateStore.Main
open Components.StoreProvider
open Components.JokePage

Jest.describe("JokePage", fun () ->

    let mutable server = MockWebServer.create ()

    Jest.beforeEach(fun _ ->
        server <- MockWebServer.create ()
        server.Start()
    )

    Jest.afterEach(fun _ ->
        server.Stop()
    )

    let jokeJson = {| value = {| joke = "hello world" |} |}

    Jest.test("loading a random joke", promise {
        TestEnv.setApiUrl (server.Url "")
        server.Stub 200 (jokeJson :> obj)

        let store = stateStore()
        let page = RTL.render(StoreProvider store (JokePage()))
        let find = page.findByText("hello world")

        do! Jest.expect(find).resolves.toBeTruthy()
    })

    Jest.test("on decode error", promise {
        TestEnv.setApiUrl (server.Url "")
        server.Stub 200 ({||} :> obj)

        let store = stateStore()
        let page = RTL.render(StoreProvider store (JokePage()))
        let find = page.findByText("There was an error loading the joke")

        do! Jest.expect(find).resolves.toBeTruthy()
    })

    Jest.test("on server error", promise {
        TestEnv.setApiUrl (server.Url "")
        server.Stub 500 ({||} :> obj)

        let store = stateStore()
        let page = RTL.render(StoreProvider store (JokePage()))
        let find = page.findByText("There was an error loading the joke")

        do! Jest.expect(find).resolves.toBeTruthy()
    })
)
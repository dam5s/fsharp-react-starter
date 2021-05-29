module ExampleTests

open Fable.Jester

Jest.describe("Hello", fun () ->
    Jest.test("when it works", fun () ->
        Jest.expect("hello world").toEqual("hello world")
    )
)

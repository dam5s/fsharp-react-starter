module TestingSupport.MockWebServer

open Fable.Core
open Fable.Core.JsInterop
open Node

// Polyfill for fetch
importAll "whatwg-fetch"

type MockWebServer =
    { Stop: unit -> unit
      Start: unit -> unit
      Url: string -> string
      Stub: int -> obj -> unit
      LastRequest: RecordedRequest option }

and RecordedRequest =
    { Method: string
      Url: string
      Body: string }

[<RequireQualifiedAccess>]
module MockWebServer =

    let create () =
        let mutable code = 200
        let mutable body = "Hello, World!"
        let mutable lastRequest : RecordedRequest option = None

        let listener (req: Http.IncomingMessage) (res: Http.ServerResponse) =
            let mutable recorded =
                { Method = req.method |> Option.defaultValue ""
                  Url = req.url |> Option.defaultValue ""
                  Body = "" }

            res.setHeader ("Access-Control-Allow-Credentials", U2.Case1 "true")
            res.setHeader ("Access-Control-Allow-Methods", U2.Case1 "GET")
            res.setHeader ("Access-Control-Allow-Origin", U2.Case1 "*")
            res.setHeader ("Access-Control-Allow-Headers", U2.Case1 "*")

            if req.method = Some "OPTIONS"
            then
                res.writeHead 200
                res.``end`` ()
            else

            res.setHeader ("Content-Type", U2.Case1 "application/json")
            res.writeHead code

            req
                .on("data", fun chunk ->
                    recorded <- { recorded with Body = $"{recorded.Body}{chunk}" }
                )
                .on("end", fun _ ->
                    lastRequest <- Some recorded
                    res.``end`` body
                )
            |> ignore

        let server = http.createServer listener

        { Stop = fun () -> server.close () |> ignore
          Start = fun () -> server.listen 0 |> ignore
          Url =
              fun path ->
                  let port = server.address()?("port")
                  $"http://localhost:{port}{path}"
          Stub =
              fun c b ->
                  code <- c
                  body <- JS.JSON.stringify b
          LastRequest = lastRequest }

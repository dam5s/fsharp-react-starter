[<RequireQualifiedAccess>]
module Networking.Http

open Fable.Core
open Thoth.Json

[<RequireQualifiedAccess>]
module private Fetch =

    type RequestInit =
        { method: string
          body: string option
          headers: Map<string, string> }

    type Response =
        abstract text : unit -> JS.Promise<string>
        abstract status : int

    [<Global>]
    let fetch (url: string, init: RequestInit) : JS.Promise<Response> = jsNative

type Request = GET of url: string

[<RequireQualifiedAccess>]
module private Request =
    let fetchArgs (GET url): string * Fetch.RequestInit =
        url, { method = "GET"; body = None; headers = Map.empty  }

type Response =
    { Status: int
      Body: string }

type Error =
    | ConnectionError of exn
    | ApiError of Response
    | ServerError of Response
    | UnknownError of Response
    | DecodeError of string

let private readResponse (response: Fetch.Response): JS.Promise<Result<Response, Error>> =
    promise {
        let! body = response.text()
        let response = { Status = response.status; Body = body }

        return
            match response.Status with
            | 200 -> Ok response
            | 400 -> Error (ApiError response)
            | 500 -> Error (ServerError response)
            | _ -> Error (UnknownError response)
    }

let private decode (decoder: Decoder<'a>) response =
    async {
        return Decode.fromString decoder response.Body |> Result.mapError DecodeError
    }

let sendRequest (request: Request): AsyncResult<Response, Error> =
    Fetch.fetch(Request.fetchArgs request)
    |> Promise.bind readResponse
    |> Promise.catch (ConnectionError >> Result.Error)
    |> Async.AwaitPromise

let sendRequestForJson (request: Request) (decoder: Decoder<'a>): AsyncResult<'a, Error> =
    sendRequest request
    |> AsyncResult.bind (decode decoder)

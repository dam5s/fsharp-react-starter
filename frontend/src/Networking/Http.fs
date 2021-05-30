[<RequireQualifiedAccess>]
module Networking.Http

open Fable.Core

[<RequireQualifiedAccess>]
module private Fetch =

    type RequestInit =
        { method: string
          body: string option
          headers: Map<string, string> }

    type Response =
        abstract json : unit -> JS.Promise<JS.Object>
        abstract status : int

    [<Global>]
    let fetch (url: string, init: RequestInit) : JS.Promise<Response> = jsNative

type Request = GET of url: string

type Response =
    { status: int
      body: JS.Object }

type Error =
    | ConnectionError of exn
    | ApiError of Response
    | ServerError of Response
    | UnknownError of Response
    | DecodeError of string

let private fetchArgs (request: Request): string * Fetch.RequestInit =
    match request with
    | GET url -> (url, { method = "GET"; body = None; headers = Map.empty })

let private readResponse (fetchResponse: Fetch.Response) =
    promise {
        let! body = fetchResponse.json()
        return { status = fetchResponse.status; body = body }
    }

let private mapResponse (response: Response) =
    match response.status with
    | 200 -> Ok response
    | 400 -> Error (ApiError response)
    | 500 -> Error (ServerError response)
    | _ -> Error (UnknownError response)

let private decode (decoder: Json.Decoder<'a>) response =
    async {
        return decoder response.body |> Result.mapError DecodeError
    }

let sendRequest (request: Request): AsyncResult<Response, Error> =
    Fetch.fetch(fetchArgs request)
    |> Promise.bind readResponse
    |> Promise.map mapResponse
    |> Promise.catch (ConnectionError >> Result.Error)
    |> Async.AwaitPromise

let sendRequestForJson (request: Request) (decoder: Json.Decoder<'a>): AsyncResult<'a, Error> =
    sendRequest request
    |> AsyncResult.bind (decode decoder)

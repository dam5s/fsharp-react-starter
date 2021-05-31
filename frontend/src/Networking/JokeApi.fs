[<RequireQualifiedAccess>]
module Networking.JokeApi

open Thoth.Json

let random (): AsyncResult<string, Http.Error> =
    let apiUrl = Env.apiUrl()
    let request = Http.GET $"%s{apiUrl}/jokes/random"
    let decoder = (Decode.field "value" (Decode.field "joke" Decode.string))

    Http.sendRequestForJson request decoder

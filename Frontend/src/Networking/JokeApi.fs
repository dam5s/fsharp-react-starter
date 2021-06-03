[<RequireQualifiedAccess>]
module Frontend.Networking.JokeApi

open Frontend
open Thoth.Json

let random (): AsyncResult<string, Http.Error> =
    let apiUrl = Env.apiUrl()
    let request = Http.GET $"%s{apiUrl}/jokes/random"
    let decoder = Decode.field "text" Decode.string

    Http.sendRequestForJson request decoder

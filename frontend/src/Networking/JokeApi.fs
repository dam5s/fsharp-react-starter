[<RequireQualifiedAccess>]
module Networking.JokeApi

let random (): AsyncResult<string, Http.Error> =
    let apiUrl = Env.apiUrl()
    let request = Http.GET $"%s{apiUrl}/jokes/random"
    let decoder = Json.objectProperty "value" (Json.property "joke")

    Http.sendRequestForJson request decoder

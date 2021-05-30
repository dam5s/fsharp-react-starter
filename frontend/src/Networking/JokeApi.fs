[<RequireQualifiedAccess>]
module Networking.JokeApi

let random (): AsyncResult<string, Http.Error> =
    let request = Http.GET "http://api.icndb.com/jokes/random"
    let decoder = Json.objectProperty "value" (Json.property "joke")

    Http.sendRequestForJson request decoder

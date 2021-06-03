module Backend.WebApp

open Giraffe

let webApp: HttpHandler =
    choose [
        GET >=> route "/jokes/random" >=> warbler (fun _ -> json (JokesRepository.random()))
        setStatusCode 404 >=> json {| message = "Not Found" |}
    ]
